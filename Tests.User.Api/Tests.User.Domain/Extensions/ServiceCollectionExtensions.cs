namespace Tests.User.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUserService, UserService>();
        serviceCollection.AddSingleton<IBookService, BookService>();

        return serviceCollection;
    }
}