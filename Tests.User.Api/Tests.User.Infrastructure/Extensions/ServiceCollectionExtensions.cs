namespace Tests.User.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DatabaseContext>();

        serviceCollection.AddSingleton<EventProcessor>();
        serviceCollection.AddSingleton<IHostedService>(p => p.GetRequiredService<EventProcessor>());
        serviceCollection.AddSingleton<IEventProcessor>(p => p.GetRequiredService<EventProcessor>());

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        return serviceCollection;
    }
}