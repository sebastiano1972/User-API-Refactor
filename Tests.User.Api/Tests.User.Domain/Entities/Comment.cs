namespace Tests.User.Domain.Entities;

public class Comment : Entity
{
    public virtual Book Book { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public virtual User Author { get; set; } = null!;
}