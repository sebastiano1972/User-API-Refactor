namespace Tests.User.Application.Features.Books.Commands.CreateBook;

internal sealed class CreateBookHandler(ILogger<CreateBookHandler> logger,
                                        IBookService bookService,
                                        IEventBag eventBag,
                                        IEventProcessor eventProcessor,
                                        IUnitOfWork unitOfWork) : IRequestHandler<CreateBookRequest, CreateBookResponse>
{
    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var book = bookService
               .CreateBook(request.Payload.Title, 
                           request.Payload.Author);

            var repository = unitOfWork
               .GetRepository<Book>();

            repository
               .Add(book);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventBag);

            return CreateBookResponse.Success(book);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return CreateBookResponse.Failure(exception);

        }
    }
}