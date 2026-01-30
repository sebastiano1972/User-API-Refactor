using Microsoft.Extensions.Logging;
using Tests.User.Application.Abstractions;

namespace Tests.User.Application.Features.Users.Commands.DeleteUser;

internal sealed class DeleteUserHandler(ILogger<DeleteUserHandler> logger, 
                                        IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            repository
               .Delete(new Domain.Entities.User
                       {
                           Id = request.Id
                       });

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return DeleteUserResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteUserResponse.Failure(exception);

        }
    }
}