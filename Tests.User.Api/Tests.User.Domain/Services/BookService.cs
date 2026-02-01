namespace Tests.User.Domain.Services;

internal sealed class BookService : IBookService
{
    public Book CreateBook(string title, string author)
    {
        return new Book
               {
                   Title = title,
                   Author = author
               };
    }

    public Book UpdateBook(Book book, string title, string author)
    {
        book.Title = title;
        book.Author = author;

        return book;
    }

    public Comment AddComment(Book book, Entities.User user, string title, string content)
    {
        var comment = new Comment
                      {
                          Title = title,
                          Content = content,
                          Author = user
                      };

        book
           .Comments
           .Add(comment);

        return comment;
    }

    public Comment? RemoveComment(Book book, int id)
    {
        var comment = book
                     .Comments
                     .SingleOrDefault(b => b.Id == id);

        if (comment != null)
        {
            book.Comments.Remove(comment);
            return comment;
        }

        return null;
    }

    public Book DeleteBook(int id)
    {
        return new Book
               {
                   Id = id
               };
    }
}