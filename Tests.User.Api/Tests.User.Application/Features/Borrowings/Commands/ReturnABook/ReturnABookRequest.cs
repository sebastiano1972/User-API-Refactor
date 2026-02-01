namespace Tests.User.Application.Features.Borrowings.Commands.ReturnABook;

public sealed record ReturnABookRequest(ReturnABookDto Payload) : IRequest<ReturnABookResponse>
{
}