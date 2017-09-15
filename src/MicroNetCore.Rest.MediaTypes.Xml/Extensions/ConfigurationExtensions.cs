using MicroNetCore.Rest.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.MediaTypes.Xml.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IRestBuilder AddXml(this IRestBuilder builder)
        {
            // Services
            builder.ServiceCollection.AddTransient<IXmlSerializer, XmlSerializer>();

            // Formatters
            builder.AddOutputFormatter(new XmlOutputFormatter());

            return builder;
        }
    }
}