namespace Tests.User.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookRequest(int Id, UpdateBookDto Payload) : IRequest<UpdateBookResponse>
{
}