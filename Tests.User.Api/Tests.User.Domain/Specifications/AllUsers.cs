namespace Tests.User.Domain.Specifications;

public sealed class AllUsers(int page, int pageSize, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Entities.User>
{
    public bool Traceable => isTraceable == IsTraceable.Yes;

    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query
              .Skip(page * pageSize)
              .Take(pageSize);
    }
}