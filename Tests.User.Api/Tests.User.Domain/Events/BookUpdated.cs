namespace Tests.User.Domain.Events;

public class BookUpdated : DomainEvent
{
    public Book Book { get; set; } = null!;
}