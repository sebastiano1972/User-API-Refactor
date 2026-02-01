using UserById = Tests.User.Application.Specifications.UserById;

namespace Tests.User.Application.Features.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(ILogger<UpdateUserHandler> logger,
                                        IUserService userService,
                                        IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var user = await repository
                            .GetByAsync(new UserById(request.Id, IsTraceable.Yes), cancellationToken)
                            .ConfigureAwait(false);

            if (user == null)
            {
                return UpdateUserResponse.Failure("User does not exist.");
            }

            userService.UpdateUser(user,
                                   request.Payload.FirstName,
                                   request.Payload.LastName,
                                   request.Payload.Age!.Value);

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