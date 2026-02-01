namespace Tests.User.Domain.Services;

internal sealed class BookService(IEventBag eventBag) : IBookService
{
    public Book CreateBook(string title, string author)
    {
        var book = new Book
                   {
                       Title = title,
                       Author = author
                   };

        eventBag
           .AddEvent(new BookCreated
                     {
                         Book = book
                     });

        return book;
    }

    public Book UpdateBook(Book book, string title, string author)
    {
        book.Title = title;
        book.Author = author;

        eventBag
           .AddEvent(new BookUpdated
                     {
                         Book = book
                     });
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

        eventBag
           .AddEvent(new CommentCreated()
                     {
                         Book = book,
                         Comment = comment
                     });

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

            eventBag
               .AddEvent(new CommentDeleted()
                         {
                             Book = book,
                             Comment = comment
                         });

            return comment;
        }

        return null;
    }

    public Book DeleteBook(int id)
    {
        var book = new Book
               {
                   Id = id
               };

        eventBag
           .AddEvent(new BookDeleted()
                     {
                         Book = book
                     });

        return book;
    }
}