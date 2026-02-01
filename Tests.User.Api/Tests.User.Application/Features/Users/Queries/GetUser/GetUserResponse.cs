namespace Tests.User.Application.Features.Users.Queries.GetUser;

public sealed record GetUserResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.User? Payload { get; }
    public bool NotFound { get; }
    public Exception? Exception { get; }

    private GetUserResponse(bool isSuccessful, bool notFound = false, Domain.Entities.User? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        NotFound = notFound;
        Payload = payload;
        Exception = exception;
    }

    public static GetUserResponse Success(Domain.Entities.User user) => new (true, payload: user);
    public static GetUserResponse Failure(Exception exception) => new(false, exception: exception);
    public static GetUserResponse UserWasNotFound() => new(false, notFound: true);
}