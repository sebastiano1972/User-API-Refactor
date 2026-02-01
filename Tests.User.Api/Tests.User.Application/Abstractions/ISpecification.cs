namespace Tests.User.Application.Abstractions;

public interface ISpecification<T> where T : class
{
    public bool Traceable { get; }
    public List<string> Includes { get; }

    IQueryable<T> Apply(IQueryable<T> query);
}