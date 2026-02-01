namespace Tests.User.Application.Features.Borrowings.Commands.ReturnABook;

public sealed record ReturnABookResponse
{
    public bool IsSuccessful { get; }
    public string Error { get; }
    public Exception? Exception { get; }

    private ReturnABookResponse(bool isSuccessful, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Error = error;
        Exception = exception;
    }

    public static ReturnABookResponse Success() => new(true);
    public static ReturnABookResponse Failure(Exception exception) => new(false, exception: exception);
    public static ReturnABookResponse Failure(string error) => new(false, error: error);
}