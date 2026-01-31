namespace Tests.User.Application.Features.Users.Queries.GetUsers;

public sealed record GetUsersRequest(int Page, int PageSize, string OrderBy) : IRequest<GetUsersResponse>
{
}