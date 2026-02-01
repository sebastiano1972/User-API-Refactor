namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentRequest(RemoveCommentDto Payload) : IRequest<DeleteCommentResponse>
{
}