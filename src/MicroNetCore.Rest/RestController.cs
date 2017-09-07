using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MicroNetCore.Models;
using MicroNetCore.Rest.Hypermedia.Models;
using MicroNetCore.Rest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroNetCore.Rest
{
    [Route("api/[controller]")]
    public abstract class RestController<TModel> : Controller
        where TModel : class, IModel, new()
    {
        private readonly IRestService<TModel> _restService;

        // ReSharper disable once PublicConstructorInAbstractClass
        public RestController(IRestService<TModel> restService)
        {
            _restService = restService;
        }

        [HttpGet]
        public async Task<Entity> Get()
        {
            var paging = GetPaging(Request);
            return paging == null
                ? await _restService.FindAsync()
                : await _restService.FindPageAsync(paging.Value.pageIndex, paging.Value.pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Entity> Get(long id)
        {
            return await _restService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TModel model)
        {
            await _restService.PostAsync(model);
            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] TModel model)
        {
            await _restService.PutAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _restService.DeleteAsync(id);
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