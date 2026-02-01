namespace Tests.User.Domain.Abstractions;

public interface IUserService
{
    Entities.User CreateUser(string firstName, string lastName, byte age);
    Entities.User UpdateUser(Entities.User user, string firstName, string lastName, byte age);
    Entities.User DeleteUser(int id);
    void BorrowBook(Entities.User user, Book book);
    void ReturnBook(Entities.User user, Book book);
}