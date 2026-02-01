namespace Tests.User.Domain.Abstractions;

public interface IBookService
{
    Book CreateBook(string title, string author);
    Book UpdateBook(Book book, string title, string author);
    Comment AddComment(Book book, Entities.User user, string title, string content);
    Comment? RemoveComment(Book book, int id);
    Book DeleteBook(int id);
}