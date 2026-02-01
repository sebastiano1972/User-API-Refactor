namespace Tests.User.Application.Specifications;

public sealed class BookById(int id, IsTraceable isTraceable = IsTraceable.No) : ISpecification<Book>
{
    public bool Traceable => isTraceable == IsTraceable.Yes;

    public List<string> Includes { get; } = ["Comments", "Comments.Author"];

    public IQueryable<Book> Apply(IQueryable<Book> query)
    {
        return query.Where(e => e.Id == id);
    }
}