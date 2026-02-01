namespace Tests.User.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.Book? Payload { get; }
    public string Error { get; }
    public Exception? Exception { get; }

    private UpdateBookResponse(bool isSuccessful, Domain.Entities.Book? payload = null, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Error = error;
        Exception = exception;
    }

    public static UpdateBookResponse Success(Domain.Entities.Book book) => new (true, payload: book);
    public static UpdateBookResponse Failure(Exception exception) => new(false, exception: exception);
    public static UpdateBookResponse Failure(string error) => new (false, error: error);
}