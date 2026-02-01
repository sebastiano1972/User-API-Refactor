namespace Tests.User.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEventBag, EventBag>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IBookService, BookService>();

        return serviceCollection;
    }
}