using Tests.User.Domain.EventSourcing;

namespace Tests.User.Application.Abstractions;

public interface IEventProcessor
{
    void Publish(EventCollection eventCollection);
}