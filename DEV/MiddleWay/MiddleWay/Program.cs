using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MiddleWay
{
    class Program
    {
        static void Main(string[] args)
        {
            startup();

            Console.WriteLine("Hello World!");
        }

        static void startup()
        {

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                //.AddSingleton<IFooService, FooService>()
                //.AddSingleton<IBarService, BarService>()
                .BuildServiceProvider();

            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>();
                //.AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            //logger.LogDebug("Starting application");


            //logger.LogDebug("All done!");

        }
    }
}
