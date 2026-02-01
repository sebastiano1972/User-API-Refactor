namespace Tests.User.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookRequest(int Id) : IRequest<DeleteBookResponse>
{
}