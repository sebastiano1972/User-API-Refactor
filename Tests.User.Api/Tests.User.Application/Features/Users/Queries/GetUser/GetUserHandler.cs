namespace Tests.User.Application.Features.Users.Queries.GetUser;

internal sealed class GetUserHandler(ILogger<GetUserHandler> logger, 
                                     ApplicationState applicationState) : IRequestHandler<GetUserRequest, GetUserResponse>
{
    public Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {

        try
        {
            if (applicationState.Users.TryGetValue(request.Id, out var user))
            {
                return Task.FromResult(GetUserResponse.Success(user));
            }

            return Task.FromResult(GetUserResponse.UserWasNotFound());
        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return Task.FromResult(GetUserResponse.Failure(exception));

        }
    }
}