using Castle.DynamicProxy;
using LoggableAttributeTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


internal static class Program
{
    
    private static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureServices((hostingContext, services) =>
        {
            var proxyGenerator = new ProxyGenerator();
            var worker = new Worker();
            var loggingInerceptor = new LoggingInterceptor();

            var workerWithProxy = proxyGenerator.CreateClassProxyWithTarget<Worker>(worker, loggingInerceptor);
            services.AddHostedService<Worker>(_ => workerWithProxy);
        });

        var app = builder.Build();
        app.RunAsync();
    }
}