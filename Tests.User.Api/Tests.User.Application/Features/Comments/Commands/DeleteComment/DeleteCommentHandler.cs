namespace Tests.User.Application.Features.Comments.Commands.DeleteComment;

internal sealed class DeleteCommentHandler(ILogger<DeleteCommentHandler> logger,
                                           IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
{
    public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            repository
               .Delete(new Domain.Entities.User
                       {
                           Id = request.Id
                       });

            await unitOfWork
                 .CompleteAsync(cancellationToken)
                 .ConfigureAwait(false);

            return DeleteCommentResponse.Success();

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return DeleteCommentResponse.Failure(exception);

        }
    }
}