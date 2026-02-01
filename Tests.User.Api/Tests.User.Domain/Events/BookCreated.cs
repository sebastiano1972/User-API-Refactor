namespace Tests.User.Domain.Events;

public class BookCreated : DomainEvent
{
    public Book Book { get; set; } = null!;
}