using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;
using MicroNetCore.Rest.Hypermedia.Services;

namespace MicroNetCore.Rest.Services
{
    public sealed class RestService<TModel> : IRestService<TModel>
        where TModel : class, IModel, new()
    {
        private readonly IHypermediaService _hypermediaService;
        private readonly IRepository<TModel> _repository;

        public RestService(
            IHypermediaService hypermediaService,
            IRepository<TModel> repository)
        {
            _hypermediaService = hypermediaService;
            _repository = repository;
        }

        public async Task<IEnumerable<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Entity> GetAsync(long id)
        {
            var model = await _repository.GetAsync(id);
            return _hypermediaService.Create(model);
        }

        public async Task<long> PostAsync(TModel model)
        {
            return await _repository.PostAsync(model);
        }

        public async Task PutAsync(long id, TModel model)
        {
            await _repository.PutAsync(id, model);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}