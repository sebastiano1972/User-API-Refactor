namespace Tests.User.Application.Features.Borrowings.Commands.ReturnABook;

internal sealed class ReturnABookHandler(ILogger<ReturnABookHandler> logger,
                                         IUserService userService,
                                         IEventBag eventBag,
                                         IEventProcessor eventProcessor,
                                         IUnitOfWork unitOfWork) : IRequestHandler<ReturnABookRequest, ReturnABookResponse>
{
    public async Task<ReturnABookResponse> Handle(ReturnABookRequest request, CancellationToken cancellationToken)
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
                return ReturnABookResponse.Failure("User not found.");
            }

            var book = await bookRepository
                          .GetByAsync(new BookById(request.Payload.BookId, IsTraceable.Yes), cancellationToken);

            if (book == null)
            {
                return ReturnABookResponse.Failure("Book not found.");
            }

            userService
               .ReturnBook(user, book);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventBag);

            return ReturnABookResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return ReturnABookResponse.Failure(exception);

        }
    }
}