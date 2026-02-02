namespace Tests.User.Api.Payloads;

public class CreateCommentPayload
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The user's last name.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(4096)]
    public string Content { get; set; } = string.Empty;
}