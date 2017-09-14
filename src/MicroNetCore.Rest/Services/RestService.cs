using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;

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

        public async Task<Entity> FindAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            var models = await _repository.FindAsync(predicate);
            return _hypermediaService.Create(models);
        }

        public async Task<Entity> FindPageAsync(int pageIndex, int pageSize,
            Expression<Func<TModel, bool>> predicate = null)
        {
            var page = await _repository.FindPageAsync(pageIndex, pageSize, predicate);
            return _hypermediaService.Create(page);
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