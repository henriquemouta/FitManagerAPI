using FitManager.Business;
using Microsoft.AspNetCore.Mvc;

namespace FITManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly Negocio<T> negocio;

        protected BaseController(Negocio<T> business)
        {
            negocio = business;
        }

        [HttpGet]
        public virtual async Task<IActionResult> getAll()
        {
            var items = await negocio.getAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var item = await negocio.getByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            await negocio.addAsync(entity);
            return Ok();
        }
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(string id, [FromBody] T entity)
        {
            await negocio.updateAsync(id, entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            await negocio.deleteAsync(id);
            return Ok();
        }
    }

}
