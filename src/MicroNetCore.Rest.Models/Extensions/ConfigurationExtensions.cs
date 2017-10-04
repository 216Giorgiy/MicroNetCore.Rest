using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Models.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelTypeProvider, ViewModelTypeProvider>();
            services.AddSingleton<IViewModelConverter, ViewModelConverter>();
            services.AddSingleton<IViewModelGenerator, ViewModelGenerator>();
            services.AddSingleton<IViewModelPropertyGenerator, ViewModelPropertyGenerator>();

            return services;
        }
    }
}