namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentResponse
{
    public bool IsSuccessful { get; }
    public string Error { get; set; }
    public Exception? Exception { get; }

    private DeleteCommentResponse(bool isSuccessful, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Error = error;
        Exception = exception;
    }

    public static DeleteCommentResponse Success() => new (true);
    public static DeleteCommentResponse Failure(Exception exception) => new(false, exception: exception);
    public static DeleteCommentResponse Failure(string error) => new(false, error: error);
}