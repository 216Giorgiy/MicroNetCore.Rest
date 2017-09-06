using System.Collections.Generic;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Hypermedia.Services
{
    public interface IHypermediaTitleGenerator
    {
        string Generate<TModel>(TModel model)
            where TModel : class, IModel;

        string Generate<TModel>(ICollection<TModel> models)
            where TModel : class, IModel;

        string Generate<TModel>(IPageCollection<TModel> page)
            where TModel : class, IModel;
    }
}