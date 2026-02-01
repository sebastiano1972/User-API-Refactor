namespace Tests.User.Application.Features.Users.Commands.CreateUser;

internal sealed class CreateUserHandler(ILogger<CreateUserHandler> logger,
                                        IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var user = request
                      .Payload
                      .ToEntity();

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            repository
               .Add(user);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return CreateUserResponse.Success(user);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return CreateUserResponse.Failure(exception);

        }
    }
}