namespace Tests.User.Application.DTOs.Book;

/// <summary>
/// Contains the data used to update an existing user.
/// </summary>
public record UpdateBookDto
{
    /// <summary>
    /// The book's title.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The book's author.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Author {get; set;} = string.Empty;
}