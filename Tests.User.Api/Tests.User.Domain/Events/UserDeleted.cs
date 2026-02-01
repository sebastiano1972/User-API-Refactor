namespace Tests.User.Domain.Events;

public class UserDeleted : DomainEvent
{
    public Entities.User User { get; set; } = null!;
}