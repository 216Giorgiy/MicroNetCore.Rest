using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Xml.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddXml(this IServiceCollection services)
        {
            services.AddTransient<IXmlSerializer, XmlSerializer>();

            return services;
        }
    }
}