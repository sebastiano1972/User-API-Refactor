namespace Tests.User.Infrastructure;

internal sealed class Repository<T>(DatabaseContext context) : IRepository<T> where T : Entity
{
    public async Task<T?> GetByAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in specification.Includes)
        {
            query = query.Include(include);
        }

        query = specification.Apply(query);

        query = specification.Traceable
                    ? query.AsTracking()
                    : query.AsNoTracking();

        return await query
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
    }

    public async Task<List<T>> GetAllByAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in specification.Includes)
        {
            query = query.Include(include);
        }

        query = specification.Apply(query);

        query = specification.Traceable
                    ? query.AsTracking()
                    : query.AsNoTracking();

        return await query
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