namespace Tests.User.Application.Features.Comments.Commands.CreateComment;

public sealed record CreateCommentResponse
{
    public bool IsSuccessful { get; }
    public Comment? Payload { get; }
    public string Error { get; }
    public Exception? Exception { get; }

    private CreateCommentResponse(bool isSuccessful, Comment? payload = null, string error = "", Exception? exception = null)
    {
        IsSuccessful = isSuccessful;
        Payload = payload;
        Error = error;
        Exception = exception;
    }

    public static CreateCommentResponse Success(Comment comment) => new(true, payload: comment);
    public static CreateCommentResponse Failure(Exception exception) => new(false, exception: exception);
    public static CreateCommentResponse Failure(string error) => new(false, error: error);
}