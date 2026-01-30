namespace Tests.User.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserResponse
{
    public bool IsSuccessful { get; }
    public Exception? Exception { get; }

    private DeleteUserResponse(bool isSuccessful, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Exception = exception;
    }

    public static DeleteUserResponse Success() => new (true);
    public static DeleteUserResponse Failure(Exception exception) => new(false, exception: exception);
}