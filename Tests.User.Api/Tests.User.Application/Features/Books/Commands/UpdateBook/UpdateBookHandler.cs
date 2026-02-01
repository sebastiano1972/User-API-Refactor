namespace Tests.User.Application.Features.Books.Commands.UpdateBook;

internal sealed class UpdateBookHandler(ILogger<UpdateBookHandler> logger, 
                                        IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.Book>();

            var book = await repository
                            .GetByAsync(new BookById(request.Id, IsTraceable.Yes), cancellationToken)
                            .ConfigureAwait(false);

            if (book == null)
            {
                return UpdateBookResponse.Failure("Book does not exist.");
            }

            book.Title = request.Payload.Title;
            book.Author = request.Payload.Author;

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return UpdateBookResponse.Success(book);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return UpdateBookResponse.Failure(exception);

        }
    }
}