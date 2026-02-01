namespace Tests.User.Application.Features.Users.Queries.GetUser;

public sealed record GetUserResponse
{
    public bool IsSuccessful { get; }
    public UserDto? Payload { get; }
    public bool NotFound { get; }
    public Exception? Exception { get; }

    private GetUserResponse(bool isSuccessful, bool notFound = false, UserDto? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        NotFound = notFound;
        Payload = payload;
        Exception = exception;
    }

    public static GetUserResponse Success(UserDto user) => new (true, payload: user);
    public static GetUserResponse Failure(Exception exception) => new(false, exception: exception);
    public static GetUserResponse UserWasNotFound() => new(false, notFound: true);
}