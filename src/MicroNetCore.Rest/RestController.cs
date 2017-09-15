using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using MicroNetCore.Rest.DataTransferObjects;
using MicroNetCore.Rest.Extensions;
using MicroNetCore.Rest.ViewModels;
using MicroNetCore.Rest.ViewModels.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel, TPost, TPut> : Controller, IRestController<TModel, TPost, TPut>
        where TModel : class, IModel, new()
        where TPost : class, IRequestViewModel<TModel>, new()
        where TPut : class, IRequestViewModel<TModel>, new()
    {
        private readonly IRepository<TModel> _repository;

        public RestController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.Create<TModel>();
        }

        #region IRestController

        [HttpGet]
        public async Task<RestObject> Get()
        {
            var query = Request.Query;

            if (query.HasPaging())
                return await GetPage(query);

            return await GetAll();
        }

        [HttpGet("{id}")]
        public async Task<RestObject> Get(long id)
        {
            return new RestModel(typeof(TModel), await _repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TPost post)
        {
            await _repository.PostAsync(post.ToModel<TPost, TModel>());
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] TPut put)
        {
            await _repository.PutAsync(id, put.ToModel<TPut, TModel>());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }

        #endregion

        #region Helpers

        private async Task<RestModels> GetAll()
        {
            return new RestModels(typeof(TModel), await _repository.FindAsync());
        }

        private async Task<RestPage> GetPage(IQueryCollection queryCollection)
        {
            var index = queryCollection.GetPageIndex();
            var size = queryCollection.GetPageSize<TModel>();

            return new RestPage(typeof(TModel), await _repository.FindPageAsync(index, size));
        }

        #endregion
    }
}