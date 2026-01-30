namespace Tests.User.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.User? Payload { get; }
    public Exception? Exception { get; }

    private CreateUserResponse(bool isSuccessful, Domain.Entities.User? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Exception = exception;
    }

    public static CreateUserResponse Success(Domain.Entities.User user) => new (true, payload: user);
    public static CreateUserResponse Failure(Exception exception) => new (false, exception: exception);
}