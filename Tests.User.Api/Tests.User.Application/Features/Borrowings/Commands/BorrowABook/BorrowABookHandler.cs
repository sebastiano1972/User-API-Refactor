using Tests.User.Application.Features.Comments.Commands.CreateComment;

namespace Tests.User.Application.Features.Borrowings.Commands.BorrowABook;

internal sealed class BorrowABookHandler(ILogger<BorrowABookHandler> logger,
                                         IUnitOfWork unitOfWork) : IRequestHandler<BorrowABookRequest, BorrowABookResponse>
{
    public async Task<BorrowABookResponse> Handle(BorrowABookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var userRepository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var bookRepository = unitOfWork
               .GetRepository<Book>();

            var user = await userRepository
                          .GetByAsync(new UserById(request.Payload.UserId, IsTraceable.Yes), cancellationToken);

            if (user == null)
            {
                return BorrowABookResponse.Failure("User not found.");
            }

            var book = await bookRepository
                          .GetByAsync(new BookById(request.Payload.BookId, IsTraceable.Yes), cancellationToken);

            if (book == null)
            {
                return BorrowABookResponse.Failure("Book not found.");
            }

            user
               .BorrowedBooks
               .Add(book);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return BorrowABookResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return BorrowABookResponse.Failure(exception);

        }
    }
}