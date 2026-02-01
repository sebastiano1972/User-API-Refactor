namespace Tests.User.Application.DTOs.User;

/// <summary>
/// Represents a user.
/// </summary>
public record UserDto
{
    /// <summary>
    /// The user's unique id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// The user's age.
    /// </summary>
    public byte Age { get; set; }

    /// <summary>
    /// The user's borrowed books.
    /// </summary>
    public List<BookListDto> BorrowedBooks { get; set; }
}