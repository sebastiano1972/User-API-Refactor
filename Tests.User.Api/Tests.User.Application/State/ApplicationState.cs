namespace Tests.User.Application.State;

internal class ApplicationState
{
    public ConcurrentDictionary<int, UserDto> Users { get; } = [];
    public ConcurrentDictionary<int, UserListDto> UserList { get; } = [];
    public ConcurrentDictionary<int, BookDto> Books { get; } = [];
    public ConcurrentDictionary<int, BookListDto> BookList { get; } = [];

    public void CreateUser(Domain.Entities.User user)
    {
        Users.TryAdd(user.Id, user.ToDto());
        UserList.TryAdd(user.Id, user.ToListDto());
    }

    public void UpdateUser(Domain.Entities.User user)
    {
        if (!Users.ContainsKey(user.Id))
        {
            return;
        }

        Users[user.Id] = user.ToDto();
        UserList[user.Id] = user.ToListDto();
    }

    public void DeleteUser(Domain.Entities.User user)
    {
        if (!Users.ContainsKey(user.Id))
        {
            return;
        }

        Users.TryRemove(user.Id, out _);
        UserList.TryRemove(user.Id, out _);
    }

    public void CreateBook(Book book)
    {
        Books.TryAdd(book.Id, book.ToDto());
        BookList.TryAdd(book.Id, book.ToListDto());
    }

    public void UpdateBook(Book book)
    {
        if (!Books.ContainsKey(book.Id))
        {
            return;
        }

        Books[book.Id] = book.ToDto();
        BookList[book.Id] = book.ToListDto();
    }

    public void DeleteBook(Book book)
    {
        if (!Books.ContainsKey(book.Id))
        {
            return;
        }

        Books.TryRemove(book.Id, out _);
        BookList.TryRemove(book.Id, out _);
    }

    public void CreateComment(Book book, Comment comment)
    {
        Books[book.Id].Comments.Add(comment.ToDto());
    }

    public void DeleteComment(Book book, Comment comment)
    {
        var commentToRemove = Books[book.Id]
                             .Comments
                             .SingleOrDefault(c => c.Id == comment.Id);

        if (commentToRemove != null)
        {
            Books[book.Id]
               .Comments
               .Remove(commentToRemove);
        }
    }

    public void BorrowBook(Book book, Domain.Entities.User user)
    {
        Users[user.Id].BorrowedBooks.Add(book.ToListDto());
    }

    public void ReturnBook(Book book, Domain.Entities.User user)
    {
        var bookToRemove = Users[user.Id]
                          .BorrowedBooks
                          .SingleOrDefault(b => b.Id == book.Id);

        if (bookToRemove != null)
        {
            Users[user.Id]
               .BorrowedBooks
               .Remove(bookToRemove);
        }
    }
}