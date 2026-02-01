namespace Tests.User.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ApplicationState>();

        serviceCollection.AddSingleton<EventProcessor>();
        serviceCollection.AddSingleton<IHostedService>(p => p.GetRequiredService<EventProcessor>());
        serviceCollection.AddSingleton<IEventProcessor>(p => p.GetRequiredService<EventProcessor>());

        serviceCollection.AddMediatR(configuration =>
                                     {
                                         configuration.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                                     });

        return serviceCollection;
    }
}