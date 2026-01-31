namespace Tests.User.Application.Features.Users.Queries.GetUsers;

public sealed record GetUsersResponse
{
    public bool IsSuccessful { get; }
    public List<Domain.Entities.User>? Payload { get; }
    public Exception? Exception { get; }

    private GetUsersResponse(bool isSuccessful, List<Domain.Entities.User>? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Exception = exception;
    }

    public static GetUsersResponse Success(List<Domain.Entities.User> users) => new (true, payload: users);
    public static GetUsersResponse Failure(Exception exception) => new(false, exception: exception);
}