namespace Tests.User.Application.Features.Borrowings.Commands.BorrowABook;

public sealed record BorrowABookResponse
{
    public bool IsSuccessful { get; }
    public string Error { get; }
    public Exception? Exception { get; }

    private BorrowABookResponse(bool isSuccessful, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Error = error;
        Exception = exception;
    }

    public static BorrowABookResponse Success() => new(true);
    public static BorrowABookResponse Failure(Exception exception) => new(false, exception: exception);
    public static BorrowABookResponse Failure(string error) => new(false, error: error);
}