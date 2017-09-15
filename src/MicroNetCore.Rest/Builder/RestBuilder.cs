using MicroNetCore.Rest.Abstractions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Builder
{
    internal sealed class RestBuilder : IRestBuilder
    {
        private readonly IMvcCoreBuilder _mvcCoreBuilder;

        public RestBuilder(IMvcCoreBuilder mvcCoreBuilder, IServiceCollection serviceCollection)
        {
            _mvcCoreBuilder = mvcCoreBuilder;
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public void AddInputFormatter(IInputFormatter formatter)
        {
            _mvcCoreBuilder.AddMvcOptions(o => o.InputFormatters.Add(formatter));
        }

        public void AddOutputFormatter(IOutputFormatter formatter)
        {
            _mvcCoreBuilder.AddMvcOptions(o => o.OutputFormatters.Add(formatter));
        }
    }
}