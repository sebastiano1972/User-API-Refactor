namespace Tests.User.Application.Features.Users.Queries.GetUsers;

public sealed record GetUsersResponse
{
    public bool IsSuccessful { get; }
    public List<UserListDto>? Payload { get; }
    public Exception? Exception { get; }

    private GetUsersResponse(bool isSuccessful, List<UserListDto>? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Exception = exception;
    }

    public static GetUsersResponse Success(List<UserListDto> users) => new (true, payload: users);
    public static GetUsersResponse Failure(Exception exception) => new(false, exception: exception);
}