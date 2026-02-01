namespace Tests.User.Application.DTOs.Book;

/// <summary>
/// Represents a book.
/// </summary>
public record BookDto
{
    /// <summary>
    /// The book's unique id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The book's title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The book's author.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// The book's comments.
    /// </summary>
    public List<CommentDto> Comments { get; set; } = [];
}