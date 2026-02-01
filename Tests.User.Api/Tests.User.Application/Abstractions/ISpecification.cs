namespace Tests.User.Application.Abstractions;

public interface ISpecification<T> where T : Entity
{
    public bool Traceable { get; }

    IQueryable<T> Apply(IQueryable<T> query);
}