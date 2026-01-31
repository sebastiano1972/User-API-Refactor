using Tests.User.Domain.Abstractions;

namespace Tests.User.Application.Abstractions;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllByAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    T Add(T entity);
    void Delete(T entity);
}