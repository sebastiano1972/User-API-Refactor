namespace Tests.User.Application.Features.Users.Queries.GetUser;

public sealed record GetUserRequest(int Id) : IRequest<GetUserResponse>
{
}