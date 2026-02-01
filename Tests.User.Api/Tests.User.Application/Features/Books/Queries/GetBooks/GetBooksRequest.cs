namespace Tests.User.Application.Features.Books.Queries.GetBooks;

public sealed record GetBooksRequest(int Page, int PageSize, string OrderBy) : IRequest<GetBooksResponse>
{
}