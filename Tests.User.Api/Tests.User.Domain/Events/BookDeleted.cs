namespace Tests.User.Domain.Events;

public class BookDeleted : DomainEvent
{
    public Book Book { get; set; } = null!;
}