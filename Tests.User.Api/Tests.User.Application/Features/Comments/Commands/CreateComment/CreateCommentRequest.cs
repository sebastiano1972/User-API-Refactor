namespace Tests.User.Application.Features.Comments.Commands.CreateComment;

public sealed record CreateCommentRequest(CreateCommentDto Payload) : IRequest<CreateCommentResponse>
{
}