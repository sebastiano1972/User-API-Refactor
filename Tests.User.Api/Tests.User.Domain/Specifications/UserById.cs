namespace Tests.User.Domain.Specifications;

public sealed class UserById(int id, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Entities.User>
{
    public bool Traceable => isTraceable == IsTraceable.Yes;

    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query.Where(e => e.Id == id);
    }
}