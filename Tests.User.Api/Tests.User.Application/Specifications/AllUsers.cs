namespace Tests.User.Application.Specifications;

public sealed class AllUsers(int page, int pageSize, string orderBy, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Domain.Entities.User>
{
    private static readonly Dictionary<string, Expression<Func<Domain.Entities.User, object?>>> Accessors = new(StringComparer.OrdinalIgnoreCase)
                                                                                                            {
                                                                                                                { "FirstName", user => user.FirstName },
                                                                                                                { "LastName", user => user.LastName },
                                                                                                            };

    public bool Traceable => isTraceable == IsTraceable.Yes;

    public List<string> Includes => [];

    public IQueryable<Domain.Entities.User> Apply(IQueryable<Domain.Entities.User> query)
    {
        return query
              .ApplyOrderBy(Accessors, orderBy)
              .Skip(page * pageSize)
              .Take(pageSize);
    }
}