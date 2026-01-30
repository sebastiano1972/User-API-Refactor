using Tests.User.Domain.Abstractions;

namespace Tests.User.Application.Abstractions;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : Entity;
    Task CompleteAsync(CancellationToken cancellationToken = default);
}