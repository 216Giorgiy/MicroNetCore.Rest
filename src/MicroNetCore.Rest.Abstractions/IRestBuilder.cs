using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNetCore.Rest.Abstractions
{
    public interface IRestBuilder
    {
        IServiceCollection ServiceCollection { get; }

        void AddInputFormatter(IInputFormatter formatter);
        void AddOutputFormatter(IOutputFormatter formatter);
    }
}