using Tests.User.Domain.EventSourcing;

namespace Tests.User.Application.Features.Users.Commands.CreateUser;

internal sealed class CreateUserHandler(ILogger<CreateUserHandler> logger,
                                        IUserService userService,
                                        EventCollection eventCollection,
                                        IEventProcessor eventProcessor,
                                        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var user = userService.CreateUser(request.Payload.FirstName,
                                              request.Payload.LastName,
                                              request.Payload.Age!.Value);

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            repository
               .Add(user);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventCollection);

            return CreateUserResponse.Success(user);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return CreateUserResponse.Failure(exception);

        }
    }
}