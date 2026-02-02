namespace Tests.User.Domain.EventSourcing;

public class EventCollection
{
    public Queue<DomainEvent> Events { get; } = new ();

    public void AddEvent(DomainEvent domainEvent)
    {
        Events.Enqueue(domainEvent);
    }
}