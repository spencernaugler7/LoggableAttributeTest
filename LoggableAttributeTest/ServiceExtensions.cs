using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace LoggableAttributeTest;

public static class ServiceExtensions
{
    public static IServiceCollection AddProxiedService<TImplementation>(this IServiceCollection services)
        where TImplementation : class
    {
        var targetService = services.Where(w => w.ImplementationType == typeof(TImplementation)).FirstOrDefault();
        if (targetService == null)
        {
            return services;
        }

        services.Remove(targetService);
        services.AddScoped<TImplementation>(provider =>
        {
            var proxyGenerator = new ProxyGenerator();
            var implementation = provider.GetRequiredService<TImplementation>();
            var loggingInerceptor = provider.GetRequiredService<LoggingInterceptor>();

            return proxyGenerator.CreateClassProxyWithTarget<TImplementation>(implementation, loggingInerceptor);
        });

        return services;
    }
}