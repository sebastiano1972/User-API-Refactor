using Tests.User.Domain.EventSourcing;

namespace Tests.User.Infrastructure.EventSourcing;

internal class EventProcessor : BackgroundService, IEventProcessor
{
    private readonly SemaphoreSlim _semaphore = new(0);
    private readonly ApplicationState _applicationState;
    private readonly ConcurrentQueue<EventCollection> _events = new();

    private readonly Dictionary<Type, Action<DomainEvent>> _eventProcessors = [];

    public EventProcessor(ApplicationState applicationState)
    {
        _applicationState = applicationState;

        _eventProcessors.Add(typeof(UserCreated), ProcessUserCreated);
        _eventProcessors.Add(typeof(UserUpdated), ProcessUserUpdated);
        _eventProcessors.Add(typeof(UserDeleted), ProcessUserDeleted);
        _eventProcessors.Add(typeof(BookCreated), ProcessBookCreated);
        _eventProcessors.Add(typeof(BookUpdated), ProcessBookUpdated);
        _eventProcessors.Add(typeof(BookDeleted), ProcessBookDeleted);
        _eventProcessors.Add(typeof(CommentCreated), ProcessCommentCreated);
        _eventProcessors.Add(typeof(CommentDeleted), ProcessCommentDeleted);
        _eventProcessors.Add(typeof(BookBorrowed), ProcessBorrowedBook);
        _eventProcessors.Add(typeof(BookReturned), ProcessReturnedBook);
    }

    public void Publish(EventCollection eventCollection)
    {
        _events.Enqueue(eventCollection);
        _semaphore.Release();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _semaphore
                   .WaitAsync(stoppingToken);

                if (!_events.TryDequeue(out var eventsCollection))
                {
                    continue;
                }

                foreach (var @event in eventsCollection.Events)
                {
                    _eventProcessors[@event.GetType()](@event);
                }
            }
        }
        catch
        {
            // Ignore
        }
    }

    private void ProcessUserCreated(DomainEvent @event)
    {
        var userCreated = @event as UserCreated;

        _applicationState.CreateUser(userCreated!.User);
    }

    private void ProcessUserUpdated(DomainEvent @event)
    {
        var userUpdated = @event as UserUpdated;

        _applicationState.UpdateUser(userUpdated!.User);
    }

    private void ProcessUserDeleted(DomainEvent @event)
    {
        var userDeleted = @event as UserDeleted;

        _applicationState.DeleteUser(userDeleted!.User);
    }

    private void ProcessBookCreated(DomainEvent @event)
    {
        var bookCreated = @event as BookCreated;

        _applicationState.CreateBook(bookCreated!.Book);
    }

    private void ProcessBookUpdated(DomainEvent @event)
    {
        var bookUpdated = @event as BookUpdated;

        _applicationState.UpdateBook(bookUpdated!.Book);
    }

    private void ProcessBookDeleted(DomainEvent @event)
    {
        var bookDeleted = @event as BookDeleted;

        _applicationState.DeleteBook(bookDeleted!.Book);
    }

    private void ProcessCommentCreated(DomainEvent @event)
    {
        var commentCreated = @event as CommentCreated;

        _applicationState.CreateComment(commentCreated!.Book, commentCreated!.Comment);
    }

    private void ProcessCommentDeleted(DomainEvent @event)
    {
        var commentDeleted = @event as CommentDeleted;

        _applicationState.DeleteComment(commentDeleted!.Book, commentDeleted!.Comment);
    }

    private void ProcessBorrowedBook(DomainEvent @event)
    {
        var bookBorrowed = @event as BookBorrowed;

        _applicationState.BorrowBook(bookBorrowed!.Book, bookBorrowed!.User);
    }

    private void ProcessReturnedBook(DomainEvent @event)
    {
        var bookReturned = @event as BookReturned;

        _applicationState.ReturnBook(bookReturned!.Book, bookReturned!.User);
    }
}