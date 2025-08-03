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
            services.AddHostedService<Worker>();
        });

        var app = builder.Build();
        app.RunAsync();
    }
}