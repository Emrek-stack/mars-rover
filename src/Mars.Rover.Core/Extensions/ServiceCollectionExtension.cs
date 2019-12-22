using Mars.Rover.Core.Domain;
using Mars.Rover.Core.Domain.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Mars.Rover.Core.Extensions
{
    public static class ServiceCollectionExtension 
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPlanet, Domain.Impl.Mars>()
                .AddScoped<IPlateau, Plateau>()
                .AddScoped<ISpaceAgency, Nasa>();

            return services;
        }
    }
}
