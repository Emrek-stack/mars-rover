using System;
using Mars.Rover.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Mars.Rover.ControlCenter
{
    class Program
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies();
        }

        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddDependencies()
                .AddScoped<App>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            app.Init();

            Console.Read();
		}
    }
}
