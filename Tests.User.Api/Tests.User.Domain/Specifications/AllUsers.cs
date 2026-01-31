using System.Linq.Expressions;
using Tests.User.Domain.Extensions;

namespace Tests.User.Domain.Specifications;

public sealed class AllUsers(int page, int pageSize, string orderBy, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Entities.User>
{
    private static readonly Dictionary<string, Expression<Func<Entities.User, object?>>> Accessors = new(StringComparer.OrdinalIgnoreCase)
                                                                                                     {
                                                                                                         { "FirstName", user => user.FirstName },
                                                                                                         { "LastName", user => user.LastName },
                                                                                                     };

    public bool Traceable => isTraceable == IsTraceable.Yes;

    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query
              .ApplyOrderBy(Accessors, orderBy)
              .Skip(page * pageSize)
              .Take(pageSize);
    }
}