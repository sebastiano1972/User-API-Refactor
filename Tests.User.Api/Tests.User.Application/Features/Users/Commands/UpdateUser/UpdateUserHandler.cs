using Microsoft.Extensions.Logging;
using Tests.User.Application.Abstractions;

namespace Tests.User.Application.Features.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(ILogger<UpdateUserHandler> logger, 
                                        IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var user = await repository
                            .GetByIdAsync(request.Id, cancellationToken)
                            .ConfigureAwait(false);

            if (user == null)
            {
                return UpdateUserResponse.Failure("User does not exist.");
            }

            user.FirstName = request.Payload.FirstName;
            user.LastName = request.Payload.LastName;
            user.Age = request.Payload.Age!.Value;

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return UpdateUserResponse.Success(user);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return UpdateUserResponse.Failure(exception);

        }
    }
}