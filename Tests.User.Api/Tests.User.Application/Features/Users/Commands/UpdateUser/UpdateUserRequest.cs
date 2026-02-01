namespace Tests.User.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserRequest(int Id, UpdateUserDto Payload) : IRequest<UpdateUserResponse>
{
}