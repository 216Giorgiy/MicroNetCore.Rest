using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.ViewModels.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<IViewModelGenerator, ViewModelGenerator>();

            return services;
        }
    }
}