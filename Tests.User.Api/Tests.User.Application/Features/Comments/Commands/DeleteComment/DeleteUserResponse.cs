namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentResponse
{
    public bool IsSuccessful { get; }
    public Exception? Exception { get; }

    private DeleteCommentResponse(bool isSuccessful, Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Exception = exception;
    }

    public static DeleteCommentResponse Success() => new (true);
    public static DeleteCommentResponse Failure(Exception exception) => new(false, exception: exception);
}