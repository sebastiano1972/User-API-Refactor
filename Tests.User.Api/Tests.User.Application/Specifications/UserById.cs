namespace Tests.User.Application.Specifications;

public sealed class UserById(int id, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Domain.Entities.User>
{
    public bool Traceable => isTraceable == IsTraceable.Yes;

    public List<string> Includes { get; } = ["BorrowedBooks"];

    public IQueryable<Domain.Entities.User> Apply(IQueryable<Domain.Entities.User> query)
    {
        return query.Where(e => e.Id == id);
    }
}