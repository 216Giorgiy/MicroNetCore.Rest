using System.Threading.Tasks;
using MicroNetCore.Models;
using MicroNetCore.Rest.Services;
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