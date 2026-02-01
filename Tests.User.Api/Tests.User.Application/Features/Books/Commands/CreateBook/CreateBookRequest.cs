namespace Tests.User.Application.Features.Books.Commands.CreateBook;

public sealed record CreateBookRequest(CreateBookDto Payload) : IRequest<CreateBookResponse>
{
}