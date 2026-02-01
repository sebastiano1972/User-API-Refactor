namespace Tests.User.Application.Features.Books.Commands.UpdateBook;

internal sealed class UpdateBookHandler(ILogger<UpdateBookHandler> logger,
                                        IBookService bookService,
                                        IEventBag eventBag,
                                        IEventProcessor eventProcessor,
                                        IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Book>();

            var book = await repository
                            .GetByAsync(new BookById(request.Id, IsTraceable.Yes), cancellationToken)
                            .ConfigureAwait(false);

            if (book == null)
            {
                return UpdateBookResponse.Failure("Book does not exist.");
            }

            bookService
               .UpdateBook(book, 
                           request.Payload.Title, 
                           request.Payload.Author);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventBag);

            return UpdateBookResponse.Success(book);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return UpdateBookResponse.Failure(exception);

        }
    }
}