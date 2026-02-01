namespace Tests.User.Application.Features.Books.Queries.GetBook;

public sealed record GetBookResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.Book? Payload { get; }
    public bool NotFound { get; }
    public Exception? Exception { get; }

    private GetBookResponse(bool isSuccessful, bool notFound = false, Domain.Entities.Book? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        NotFound = notFound;
        Payload = payload;
        Exception = exception;
    }

    public static GetBookResponse Success(Domain.Entities.Book book) => new (true, payload: book);
    public static GetBookResponse Failure(Exception exception) => new(false, exception: exception);
    public static GetBookResponse BookWasNotFound() => new(false, notFound: true);
}