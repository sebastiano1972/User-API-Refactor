namespace Tests.User.Domain.Specifications;

public sealed class UserById(int id) : ISpecification<Entities.User>
{
    public bool Traceable => false;

    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query.Where(e => e.Id == id);
    }
}