namespace Tests.User.Application.DTOs.Book;

/// <summary>
/// Represents a book in a list of books.
/// </summary>
public record BookListDto
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
}