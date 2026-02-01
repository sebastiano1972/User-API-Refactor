namespace Tests.User.Domain.Events;

public class UserUpdated : DomainEvent
{
    public Entities.User User { get; set; } = null!;
}