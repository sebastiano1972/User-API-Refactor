namespace Tests.User.Api.Extensions;

internal static class HostExtensions
{
    public static async Task SeedDb(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();
        var bookRepository = unitOfWork.GetRepository<Book>();

        userRepository.Add(new Domain.Entities.User
                           {
                               FirstName = "Sebastiano",
                               LastName = "Serri",
                               Age = 54
                           });

        bookRepository.Add(new Book
                           {
                               Title = "Dune",
                               Author = "Frank Herbert"
                           });

        bookRepository.Add(new Book
                           {
                               Title = "The heretics of Dune",
                               Author = "Frank Herbert"
                           });

        await unitOfWork
           .CompleteAsync();
    }
}