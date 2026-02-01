namespace Tests.User.Application.DTOs.Borrowings;

/// <summary>
/// Contains the data used to borrow a book.
/// </summary>
public record BorrowABookDto
{
    /// <summary>
    /// The id of the user borrowing a book.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// The id of the borrowed book.
    /// </summary>
    [Required]
    public int BookId {get; set;}
}