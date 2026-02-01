namespace Tests.User.Application.Specifications;

public sealed class AllBooks(int page, int pageSize, string orderBy, IsTraceable isTraceable = IsTraceable.No) : ISpecification<BookListDto>
{
    private static readonly Dictionary<string, Expression<Func<BookListDto, object?>>> Accessors = new(StringComparer.OrdinalIgnoreCase)
                                                                                                   {
                                                                                                       { "Title", user => user.Title },
                                                                                                       { "Author", user => user.Author }
                                                                                                   };

    public bool Traceable => isTraceable == IsTraceable.Yes;

    public List<string> Includes => [];

    public IQueryable<BookListDto> Apply(IQueryable<BookListDto> query)
    {
        return query
              .ApplyOrderBy(Accessors, orderBy)
              .Skip(page * pageSize)
              .Take(pageSize);
    }
}