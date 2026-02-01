namespace Tests.User.Application.Abstractions;

public interface IEventProcessor
{
    void Publish(IEventBag eventBag);
}