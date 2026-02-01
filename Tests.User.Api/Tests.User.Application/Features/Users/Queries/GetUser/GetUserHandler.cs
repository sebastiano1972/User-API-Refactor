using UserById = Tests.User.Application.Specifications.UserById;

namespace Tests.User.Application.Features.Users.Queries.GetUser;

internal sealed class GetUserHandler(ILogger<GetUserHandler> logger, 
                                     IUnitOfWork unitOfWork) : IRequestHandler<GetUserRequest, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var user = await repository
                            .GetByAsync(new UserById(request.Id), cancellationToken)
                            .ConfigureAwait(false);

            return user == null
                       ? GetUserResponse.UserWasNotFound()
                       : GetUserResponse.Success(user);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return GetUserResponse.Failure(exception);

        }
    }
}