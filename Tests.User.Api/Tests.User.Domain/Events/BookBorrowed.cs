namespace Tests.User.Domain.Events;

public class BookBorrowed : DomainEvent
{
    public Book Book { get; set; } = null!;
    public Entities.User User { get; set; } = null!;
}