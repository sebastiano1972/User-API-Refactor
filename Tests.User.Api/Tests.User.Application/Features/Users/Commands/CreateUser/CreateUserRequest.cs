using Tests.User.Application.DTOs.User;

namespace Tests.User.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserRequest(CreateUserDto Payload) : IRequest<CreateUserResponse>
{
}