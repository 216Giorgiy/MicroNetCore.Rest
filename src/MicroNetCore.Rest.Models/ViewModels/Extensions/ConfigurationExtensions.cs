using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Models.ViewModels.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelTypeProvider, ViewModelTypeProvider>();
            services.AddTransient<IViewModelGenerator, ViewModelGenerator>();

            return services;
        }
    }
}