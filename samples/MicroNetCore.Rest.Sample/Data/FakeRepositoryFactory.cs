using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

namespace MicroNetCore.Rest.Sample.Data
{
    public sealed class FakeRepositoryFactory : IRepositoryFactory
    {
        public IRepository<TModel> Create<TModel>()
            where TModel : class, IModel, new()
        {
            return new FakeRepository<TModel>();
        }
    }
}