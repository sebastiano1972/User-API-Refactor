namespace Tests.User.Application.Features.Comments.Commands.CreateComment;

internal sealed class CreateCommentHandler(ILogger<CreateCommentHandler> logger,
                                           IBookService bookService,
                                           IEventBag eventBag,
                                           IEventProcessor eventProcessor,
                                           IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentRequest, CreateCommentResponse>
{
    public async Task<CreateCommentResponse> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var userRepository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var bookRepository = unitOfWork
               .GetRepository<Book>();

            var author = await userRepository
                          .GetByAsync(new UserById(request.Payload.UserId, IsTraceable.Yes), cancellationToken);

            if (author == null)
            {
                return CreateCommentResponse.Failure("User not found.");
            }

            var book = await bookRepository
                          .GetByAsync(new BookById(request.Payload.BookId, IsTraceable.Yes), cancellationToken);

            if (book == null)
            {
                return CreateCommentResponse.Failure("Book not found.");
            }

            var comment = bookService
               .AddComment(book,
                           author,
                           request.Payload.Title,
                           request.Payload.Content);
            
            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            eventProcessor
               .Publish(eventBag);

            return CreateCommentResponse.Success(comment);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return CreateCommentResponse.Failure(exception);

        }
    }
}