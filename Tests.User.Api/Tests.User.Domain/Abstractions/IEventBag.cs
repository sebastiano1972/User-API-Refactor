namespace Tests.User.Domain.Abstractions;

public interface IEventBag
{
    public Queue<DomainEvent> Events { get; }

    void AddEvent(DomainEvent domainEvent);
}