namespace Tests.User.Infrastructure;

internal sealed class Repository<T>(DatabaseContext context) : IRepository<T> where T : Entity
{
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context
                    .Set<T>()
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
                    .ConfigureAwait(false);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context
                    .Set<T>()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
    }

    public T Add(T entity)
    {
        context
           .Set<T>()
           .Add(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        context
           .Set<T>()
           .Remove(entity);
    }
}