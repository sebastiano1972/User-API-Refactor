namespace Tests.User.Application.Features.Books.Commands.CreateBook;

public sealed record CreateBookResponse
{
    public bool IsSuccessful { get; }
    public Domain.Entities.Book? Payload { get; }
    public Exception? Exception { get; }

    private CreateBookResponse(bool isSuccessful, Domain.Entities.Book? payload = null, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Exception = exception;
    }

    public static CreateBookResponse Success(Domain.Entities.Book book) => new (true, payload: book);
    public static CreateBookResponse Failure(Exception exception) => new (false, exception: exception);
}