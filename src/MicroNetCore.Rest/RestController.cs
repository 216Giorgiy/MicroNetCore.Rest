using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// RESTful controller need to implement HATEOAS principles. This is one of the main tasks to complete.
// Also need to add filtering and paging.

namespace MicroNetCore.Rest
{
    [RestController]
    public abstract class RestController<TModel> : Controller
        where TModel : class, new()
    {
        private readonly IRestService<TModel> _restService;

        protected RestController(IRestService<TModel> restService)
        {
            _restService = restService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await _restService.FindAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var model = await _restService.GetAsync(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TModel model)
        {
            await _restService.PostAsync(model);
            return Ok();
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
    }
}