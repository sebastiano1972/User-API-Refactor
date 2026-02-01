namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentRequest(int Id) : IRequest<DeleteCommentResponse>
{
}