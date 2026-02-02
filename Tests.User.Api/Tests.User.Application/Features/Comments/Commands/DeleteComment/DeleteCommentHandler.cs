using Tests.User.Domain.EventSourcing;

namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

internal sealed class DeleteCommentHandler(ILogger<DeleteCommentHandler> logger,
                                           IBookService bookService,
                                           EventCollection eventCollection,
                                           IEventProcessor eventProcessor,
                                           IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
{
    public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var repository = unitOfWork
               .GetRepository<Book>();

            var book = await repository
                          .GetByAsync(new BookById(request.Payload.BookId, IsTraceable.Yes), cancellationToken);

            if (book == null)
            {
                return DeleteCommentResponse.Failure("Book does not exist.");
            }

            bookService
               .RemoveComment(book, 
                              request.Payload.CommentId);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventCollection);

            return DeleteCommentResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteCommentResponse.Failure(exception);

        }
    }
}