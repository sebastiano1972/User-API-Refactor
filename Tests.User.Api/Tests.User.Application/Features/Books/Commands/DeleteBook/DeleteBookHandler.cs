using Tests.User.Domain.EventSourcing;

namespace Tests.User.Application.Features.Books.Commands.DeleteBook;

internal sealed class DeleteBookHandler(ILogger<DeleteBookHandler> logger,
                                        IBookService bookService,
                                        EventCollection eventCollection,
                                        IEventProcessor eventProcessor,
                                        IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookRequest, DeleteBookResponse>
{
    public async Task<DeleteBookResponse> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Book>();

            repository
               .Delete(bookService.DeleteBook(request.Id));

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventCollection);

            return DeleteBookResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteBookResponse.Failure(exception);

        }
    }
}