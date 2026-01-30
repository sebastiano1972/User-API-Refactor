namespace Tests.User.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.User? Payload { get; }
    public string Error { get; }
    public Exception? Exception { get; }

    private UpdateUserResponse(bool isSuccessful, Domain.Entities.User? payload = null, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Error = error;
        Exception = exception;
    }

    public static UpdateUserResponse Success(Domain.Entities.User user) => new (true, payload: user);
    public static UpdateUserResponse Failure(Exception exception) => new(false, exception: exception);
    public static UpdateUserResponse Failure(string error) => new (false, error: error);
}