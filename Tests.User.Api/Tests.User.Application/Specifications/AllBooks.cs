namespace Tests.User.Application.Specifications;

public sealed class AllBooks(int page, int pageSize, string orderBy, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Book>
{
    private static readonly Dictionary<string, Expression<Func<Book, object?>>> Accessors = new(StringComparer.OrdinalIgnoreCase)
                                                                                                            {
                                                                                                                { "Title", user => user.Title },
                                                                                                                { "Author", user => user.Author }
                                                                                                            };

    public bool Traceable => isTraceable == IsTraceable.Yes;

    public List<string> Includes => [];

    public IQueryable<Book> Apply(IQueryable<Book> query)
    {
        return query
              .ApplyOrderBy(Accessors, orderBy)
              .Skip(page * pageSize)
              .Take(pageSize);
    }
}