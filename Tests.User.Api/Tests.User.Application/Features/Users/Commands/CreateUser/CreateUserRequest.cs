namespace Tests.User.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserRequest(CreateUserDto Payload) : IRequest<CreateUserResponse>
{
}