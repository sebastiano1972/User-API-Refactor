namespace Tests.User.Application.DTOs.User;

/// <summary>
/// Contains the data used to create a user.
/// </summary>
public record CreateUserDto
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The user's last name.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string LastName {get; set;} = string.Empty;

    /// <summary>
    /// The user's age.
    /// </summary>
    [Required]
    [Range(0, 120)]
    public byte? Age { get; set; }
}