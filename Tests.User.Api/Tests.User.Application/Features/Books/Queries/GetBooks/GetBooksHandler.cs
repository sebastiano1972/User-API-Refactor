namespace Tests.User.Application.Features.Books.Queries.GetBooks;

internal sealed class GetBooksHandler(ILogger<GetBooksHandler> logger,
                                      IUnitOfWork unitOfWork) : IRequestHandler<GetBooksRequest, GetBooksResponse>
{
    public async Task<GetBooksResponse> Handle(GetBooksRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var repository = unitOfWork
               .GetRepository<Book>();

            var books = await repository
                            .GetAllByAsync(new AllBooks(request.Page, request.PageSize, request.OrderBy), cancellationToken)
                            .ConfigureAwait(false);

            return GetBooksResponse.Success(books);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return GetBooksResponse.Failure(exception);

        }
    }
}