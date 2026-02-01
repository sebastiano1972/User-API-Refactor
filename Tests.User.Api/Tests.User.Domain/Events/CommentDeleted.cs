namespace Tests.User.Domain.Events;

public class CommentDeleted : DomainEvent
{
    public Book Book { get; set; } = null!;
    public Comment Comment { get; set; } = null!;
}