namespace Tests.User.Domain.Entities;

public class Book : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public virtual List<User> Users { get; set; } = new();
    public virtual List<Comment> Comments { get; set; } = new();
}