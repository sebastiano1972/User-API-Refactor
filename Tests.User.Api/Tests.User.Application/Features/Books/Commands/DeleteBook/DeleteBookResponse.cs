namespace Tests.User.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookResponse
{
    public bool IsSuccessful { get; }
    public Exception? Exception { get; }

    private DeleteBookResponse(bool isSuccessful, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Exception = exception;
    }

    public static DeleteBookResponse Success() => new (true);
    public static DeleteBookResponse Failure(Exception exception) => new(false, exception: exception);
}