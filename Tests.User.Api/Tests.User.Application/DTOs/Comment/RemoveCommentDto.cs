namespace Tests.User.Application.DTOs.Comment;

/// <summary>
/// Contains the data used to remove a comment.
/// </summary>
public record RemoveCommentDto
{
    /// <summary>
    /// The id of the book.
    /// </summary>
    [Required]
    public int BookId { get; set; }

    /// <summary>
    /// The id of the comment.
    /// </summary>
    [Required]
    public int CommentId { get; set; }
}