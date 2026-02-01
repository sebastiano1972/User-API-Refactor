namespace Tests.User.Application.Features.Borrowings.Commands.BorrowABook;

public sealed record BorrowABookRequest(BorrowABookDto Payload) : IRequest<BorrowABookResponse>
{
}