namespace Tests.User.Application.Features.Users.Queries.GetUsers;

internal sealed class GetUsersHandler(ILogger<GetUsersHandler> logger,
                                      ApplicationState applicationState) : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    public Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var allUser = new AllUsers(request.Page, request.PageSize, request.OrderBy);

            var userList = allUser
               .Apply(applicationState.UserList.Values.AsQueryable());

            return Task.FromResult(GetUsersResponse.Success(userList.ToList()));
        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return Task.FromResult(GetUsersResponse.Failure(exception));

        }
    }
}