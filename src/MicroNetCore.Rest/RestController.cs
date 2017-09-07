using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel> : Controller
        where TModel : class, IModel, new()
    {
        private readonly IRepository<TModel> _repository;

        // ReSharper disable once PublicConstructorInAbstractClass
        public RestController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.Create<TModel>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var paging = GetPaging(Request);

            return Ok(paging == null
                ? await _repository.FindAsync()
                : await _repository.FindPageAsync(paging.Value.pageIndex, paging.Value.pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TModel model)
        {
            await _repository.PostAsync(model);
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] TModel model)
        {
            await _repository.PutAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }

        private static (int pageIndex, int pageSize)? GetPaging(HttpRequest request)
        {
            var index = int.TryParse(request.Query["pageIndex"].SingleOrDefault(), out var pageIndex);
            var size = int.TryParse(request.Query["pageSize"].SingleOrDefault(), out var pageSize);

            if (!index && !size)
                return null;

            if (!index) pageIndex = 1;
            if (!size) pageSize = 20;

            return (pageIndex, pageSize);
        }
    }
}