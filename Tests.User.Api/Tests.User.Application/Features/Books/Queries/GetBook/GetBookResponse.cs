namespace Tests.User.Application.Features.Books.Queries.GetBook;

public sealed record GetBookResponse
{
    public bool IsSuccessful { get; }
    public Book? Payload { get; }
    public bool NotFound { get; }
    public Exception? Exception { get; }

    private GetBookResponse(bool isSuccessful, bool notFound = false, Book? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        NotFound = notFound;
        Payload = payload;
        Exception = exception;
    }

    public static GetBookResponse Success(Book book) => new (true, payload: book);
    public static GetBookResponse Failure(Exception exception) => new(false, exception: exception);
    public static GetBookResponse BookWasNotFound() => new(false, notFound: true);
}