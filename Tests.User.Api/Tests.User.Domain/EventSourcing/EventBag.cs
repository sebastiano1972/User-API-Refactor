namespace Tests.User.Domain.EventSourcing;

internal class EventBag : IEventBag
{
    public Queue<DomainEvent> Events { get; } = new ();

    public void AddEvent(DomainEvent domainEvent)
    {
        Events.Enqueue(domainEvent);
    }
}