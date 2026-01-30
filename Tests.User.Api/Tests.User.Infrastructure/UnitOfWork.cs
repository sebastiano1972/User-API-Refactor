namespace Tests.User.Infrastructure;

internal sealed class UnitOfWork(DatabaseContext context) : IUnitOfWork
{
    public IRepository<T> GetRepository<T>() where T : Entity
    {
        return new Repository<T>(context);
    }

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context
                 .SaveChangesAsync(cancellationToken)
                 .ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            when (dbUpdateConcurrencyException.Message == "Attempted to update or delete an entity that does not exist in the store.")
        {
            // Ignore as deletion is idempotent
        }
    }
}