namespace Tests.User.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserRequest(int Id) : IRequest<DeleteUserResponse>
{
}