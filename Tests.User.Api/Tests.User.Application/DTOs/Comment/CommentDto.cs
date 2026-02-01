namespace Tests.User.Application.DTOs.Comment;

/// <summary>
/// Represents a user.
/// </summary>
public record CommentDto
{
    /// <summary>
    /// The comment's unique id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The comment's title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The comment's content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The comment's author.
    /// </summary>
    public string Author { get; set; } = string.Empty;
}