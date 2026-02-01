namespace Tests.User.Application.Features.Books.Queries.GetBook;

public sealed record GetBookRequest(int Id) : IRequest<GetBookResponse>
{
}