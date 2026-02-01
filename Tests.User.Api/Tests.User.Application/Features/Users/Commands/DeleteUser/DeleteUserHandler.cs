namespace Tests.User.Application.Features.Users.Commands.DeleteUser;

internal sealed class DeleteUserHandler(ILogger<DeleteUserHandler> logger, 
                                        IUserService userService,
                                        IEventBag eventBag,
                                        IEventProcessor eventProcessor,
                                        IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            repository
               .Delete(userService.DeleteUser(request.Id));

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventBag);

            return DeleteUserResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteUserResponse.Failure(exception);

        }
    }
}