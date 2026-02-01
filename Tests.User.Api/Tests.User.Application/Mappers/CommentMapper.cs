namespace Tests.User.Application.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
               {
                   Id = comment.Id,
                   Title = comment.Title,
                   Content = comment.Content,
                   Author = $"{comment.Author.FirstName} {comment.Author.LastName}"
               };
    }
}