namespace Tests.User.Application.Features.Books.Commands.DeleteBook;

internal sealed class DeleteBookHandler(ILogger<DeleteBookHandler> logger, 
                                        IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookRequest, DeleteBookResponse>
{
    public async Task<DeleteBookResponse> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.Book>();

            repository
               .Delete(new Domain.Entities.Book
                       {
                           Id = request.Id
                       });

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return DeleteBookResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteBookResponse.Failure(exception);

        }
    }
}