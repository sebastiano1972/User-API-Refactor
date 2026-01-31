using Microsoft.Extensions.Logging;
using Tests.User.Application.Abstractions;
using Tests.User.Domain.Specifications;

namespace Tests.User.Application.Features.Users.Queries.GetUsers;

internal sealed class GetUsersHandler(
    ILogger<GetUsersHandler> logger,
    IUnitOfWork unitOfWork) : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var repository = unitOfWork
               .GetRepository<Domain.Entities.User>();

            var users = await repository
                            .GetAllByAsync(new AllUsers(request.Page, request.PageSize, request.OrderBy), cancellationToken)
                            .ConfigureAwait(false);

            return GetUsersResponse.Success(users);

        }
        catch (Exception exception)
        {

            logger.LogError(exception, exception.Message);

            return GetUsersResponse.Failure(exception);

        }
    }
}