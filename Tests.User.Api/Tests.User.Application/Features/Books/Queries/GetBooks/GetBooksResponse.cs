namespace Tests.User.Application.Features.Books.Queries.GetBooks;

public sealed record GetBooksResponse
{
    public bool IsSuccessful { get; }
    public List<Book>? Payload { get; }
    public Exception? Exception { get; }

    private GetBooksResponse(bool isSuccessful, List<Book>? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Exception = exception;
    }

    public static GetBooksResponse Success(List<Book> books) => new (true, payload: books);
    public static GetBooksResponse Failure(Exception exception) => new(false, exception: exception);
}