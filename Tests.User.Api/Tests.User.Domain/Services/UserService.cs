namespace Tests.User.Domain.Services;

internal sealed class UserService(IEventBag eventBag) : IUserService
{
    public Entities.User CreateUser(string firstName, string lastName, byte age)
    {
        var user = new Entities.User
                   {
                       FirstName = firstName,
                       LastName = lastName,
                       Age = age
                   };

        eventBag
           .AddEvent(new UserCreated
                     {
                         User = user
                     });

        return user;
    }

    public Entities.User UpdateUser(Entities.User user, string firstName, string lastName, byte age)
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        user.Age = age;

        eventBag
           .AddEvent(new UserUpdated
                     {
                         User = user
                     });

        return user;
    }

    public Entities.User DeleteUser(int id)
    {
        var user = new Entities.User
                   {
                       Id = id
                   };

        eventBag
           .AddEvent(new UserDeleted()
                     {
                         User = user
                     });

        return user;
    }

    public void BorrowBook(Entities.User user, Book book)
    {
        user
           .BorrowedBooks
           .Add(book);

        eventBag
           .AddEvent(new BookBorrowed()
                     {
                         User = user,
                         Book = book
                     });
    }

    public void ReturnBook(Entities.User user, Book book)
    {
        user
           .BorrowedBooks
           .Remove(book);

        eventBag
           .AddEvent(new BookReturned()
                     {
                         User = user,
                         Book = book
                     });
    }
}