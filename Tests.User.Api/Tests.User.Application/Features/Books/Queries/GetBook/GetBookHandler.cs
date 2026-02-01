namespace Tests.User.Application.Features.Books.Queries.GetBook;

internal sealed class GetBookHandler(ILogger<GetBookHandler> logger, 
                                     IUnitOfWork unitOfWork) : IRequestHandler<GetBookRequest, GetBookResponse>
{
    public async Task<GetBookResponse> Handle(GetBookRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var repository = unitOfWork
               .GetRepository<Domain.Entities.Book>();

            var book = await repository
                            .GetByAsync(new BookById(request.Id), cancellationToken)
                            .ConfigureAwait(false);

            return book == null
                       ? GetBookResponse.BookWasNotFound()
                       : GetBookResponse.Success(book);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return GetBookResponse.Failure(exception);

        }
    }
}