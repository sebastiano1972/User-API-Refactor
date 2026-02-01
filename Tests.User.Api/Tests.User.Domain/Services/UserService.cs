namespace Tests.User.Domain.Services;

internal sealed class UserService : IUserService
{
    public Entities.User CreateUser(string firstName, string lastName, byte age)
    {
        return new Entities.User
               {
                   FirstName = firstName,
                   LastName = lastName,
                   Age = age
               };
    }

    public Entities.User UpdateUser(Entities.User user, string firstName, string lastName, byte age)
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        user.Age = age;

        return user;
    }

    public Entities.User DeleteUser(int id)
    {
        return new Entities.User
               {
                   Id = id
               };
    }

    public void BorrowBook(Entities.User user, Book book)
    {
        user
           .BorrowedBooks
           .Add(book);
    }

    public void ReturnBook(Entities.User user, Book book)
    {
        user
           .BorrowedBooks
           .Remove(book);
    }
}