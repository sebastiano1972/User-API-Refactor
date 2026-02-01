using Tests.User.Application.Features.Users.Queries.GetUser;

namespace Tests.User.Application.Features.Books.Queries.GetBook;

internal sealed class GetBookHandler(ILogger<GetBookHandler> logger,
                                     ApplicationState applicationState) : IRequestHandler<GetBookRequest, GetBookResponse>
{
    public Task<GetBookResponse> Handle(GetBookRequest request, CancellationToken cancellationToken)
    {

        try
        {
            if (applicationState.Books.TryGetValue(request.Id, out var book))
            {
                return Task.FromResult(GetBookResponse.Success(book));
            }

            return Task.FromResult(GetBookResponse.BookWasNotFound());

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return Task.FromResult(GetBookResponse.Failure(exception));

        }
    }
}