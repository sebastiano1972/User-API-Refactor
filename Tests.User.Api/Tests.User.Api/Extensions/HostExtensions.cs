namespace Tests.User.Api.Extensions;

internal static class HostExtensions
{
    public static async Task SeedDb(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var bookRepository = unitOfWork.GetRepository<Book>();

        var user1 = userRepository.Add(new Domain.Entities.User
                                       {
                                           FirstName = "Sebastiano",
                                           LastName = "Serri",
                                           Age = 54
                                       });

        var user2 = userRepository
           .Add(new Domain.Entities.User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Age = 54
                });

        var user3 = userRepository
           .Add(new Domain.Entities.User
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Age = 54
                });

        var book1 = bookRepository
           .Add(new Book
                {
                    Title = "Dune",
                    Author = "Frank Herbert",
                    Comments =
                    [
                        new Comment
                        {
                            Author = user1,
                            Title = "A great book!",
                            Content = "Nothing to add."
                        },
                        new Comment
                        {
                            Author = user2,
                            Title = "Superb",
                            Content = "What should I say."
                        },
                        new Comment
                        {
                            Author = user3,
                            Title = "Boring!",
                            Content = "Cannot finish it!"
                        }
                    ]
                });

        var book2 = bookRepository
           .Add(new Book
                {
                    Title = "The heretics of Dune",
                    Author = "Frank Herbert",
                    Comments =
                    [
                        new Comment
                        {
                            Author = user1,
                            Title = "A great sequel!",
                            Content = "Adding something would be too much."
                        },
                        new Comment
                        {
                            Author = user2,
                            Title = "Not so great",
                            Content = "I liked Dune more."
                        },
                        new Comment
                        {
                            Author = user3,
                            Title = "Another boring book!",
                            Content = "Cannot finish this book either!"
                        }
                    ]
                });

        user2
           .BorrowedBooks
           .Add(book1);

        await unitOfWork
           .CompleteAsync();
    }
}