namespace Tests.User.Application.Features.Books.Queries.GetBooks;

internal sealed class GetBooksHandler(ILogger<GetBooksHandler> logger,
                                      ApplicationState applicationState) : IRequestHandler<GetBooksRequest, GetBooksResponse>
{
    public Task<GetBooksResponse> Handle(GetBooksRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var allBooks = new AllBooks(request.Page, request.PageSize, request.OrderBy);

            var bookList = allBooks
               .Apply(applicationState.BookList.Values.AsQueryable());

            return Task.FromResult(GetBooksResponse.Success(bookList.ToList()));

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return Task.FromResult(GetBooksResponse.Failure(exception));

        }
    }
}