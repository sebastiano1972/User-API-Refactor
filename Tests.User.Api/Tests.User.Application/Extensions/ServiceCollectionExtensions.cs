namespace Tests.User.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddMediatR(configuration =>
                                     {
                                         configuration.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                                     });

        return serviceCollection;
    }
}