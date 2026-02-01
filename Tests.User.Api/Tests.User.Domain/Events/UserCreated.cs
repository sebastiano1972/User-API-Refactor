namespace Tests.User.Domain.Events;

public class UserCreated : DomainEvent
{
    public Entities.User User { get; set; } = null!;
}