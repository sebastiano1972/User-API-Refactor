namespace Tests.User.Application.Features.Books.Commands.CreateBook;

internal sealed class CreateBookHandler(ILogger<CreateBookHandler> logger,
                                        IUnitOfWork unitOfWork) : IRequestHandler<CreateBookRequest, CreateBookResponse>
{
    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var book = request
                      .Payload
                      .ToEntity();

            var repository = unitOfWork
               .GetRepository<Domain.Entities.Book>();

            repository
               .Add(book);

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return CreateBookResponse.Success(book);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);
            
            return CreateBookResponse.Failure(exception);

        }
    }
}