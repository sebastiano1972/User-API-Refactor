using System.Linq.Expressions;

namespace Tests.User.Domain.Extensions;

internal static class SpecificationExtensions
{
    public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query, Dictionary<string, Expression<Func<T, object?>>> accessors, string orderBy)
    {
        var firstIteration = true;

        foreach (var propertyName in orderBy.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            if (propertyName[0] != '+' && propertyName[0] != '-')
            {
                throw new InvalidOperationException($"The ordering property {propertyName} must be prefixed by a + or a - symbol.");
            }

            if (!accessors.ContainsKey(propertyName[1..]))
            {
                throw new InvalidOperationException($"Unknown ordering property {propertyName[1..]}.");
            }

            if (firstIteration)
            {
                query = propertyName[0] == '+'
                            ? query.OrderBy(accessors[propertyName[1..]])
                            : query.OrderByDescending(accessors[propertyName[1..]]);

                firstIteration = false;
            }
            else
            {
                query = propertyName[0] == '+'
                            ? ((IOrderedQueryable<T>)query).ThenBy(accessors[propertyName[1..]])
                            : ((IOrderedQueryable<T>)query).ThenByDescending(accessors[propertyName[1..]]);
            }
        }

        return query;
    }
}