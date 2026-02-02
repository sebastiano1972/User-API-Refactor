using Tests.User.Domain.EventSourcing;

namespace Tests.User.Application.Features.Borrowings.Commands.BorrowABook;

internal sealed class BorrowABookHandler(ILogger<BorrowABookHandler> logger,
                                         IUserService userService,
                                         EventCollection eventCollection,
                                         IEventProcessor eventProcessor,
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

            userService
               .BorrowBook(user, book);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventCollection);

            return BorrowABookResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return BorrowABookResponse.Failure(exception);

        }
    }
}