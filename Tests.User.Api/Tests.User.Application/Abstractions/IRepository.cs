using Tests.User.Domain.Abstractions;

namespace Tests.User.Application.Abstractions;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    T Add(T entity);
    void Delete(T entity);
}