namespace Tests.User.Application.DTOs.Borrowings;

/// <summary>
/// Contains the data used to return a book.
/// </summary>
public record ReturnABookDto
{
    /// <summary>
    /// The id of the user returning a book.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// The id of the returned book.
    /// </summary>
    [Required]
    public int BookId {get; set;}
}