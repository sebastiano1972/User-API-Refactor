namespace Tests.User.Domain.Events;

public class BookReturned : DomainEvent
{
    public Book Book { get; set; } = null!;
    public Entities.User User { get; set; } = null!;
}