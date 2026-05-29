using FitManager.Business;
using FitManager.ViewModels;
using FitManager.ViewModels.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/instrutores")]
    public class InstrutorController : ControllerBase
    {
        private readonly NegocioUsuario negocio;
        private const int CARGO_INSTRUTOR = 2;

        public InstrutorController(NegocioUsuario negocio) { this.negocio = negocio; }

        [HttpGet]
        public async Task<IActionResult> listar(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var resultado = await negocio.listarPorCargoAsync(CARGO_INSTRUTOR, search, page, limit);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> cadastrar([FromBody] CadastroUsuarioVM vm)
        {
            try
            {
                var instrutor = await negocio.cadastrarAsync(vm);
                
                return StatusCode(201, new
                {
                    message = "Instrutor cadastrado com sucesso",
                    instrutor = new { id = instrutor.id, cargoId = instrutor.cargoId, cargo = instrutor.cargo }
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editar(int id, [FromBody] EditarUsuarioVM vm)
        {
            try
            {
                await negocio.editarAsync(id, vm);
                return Ok(new { message = "Instrutor atualizado com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuario nao encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> excluir(int id)
        {
            try
            {
                await negocio.deleteAsync(id.ToString());
                return Ok(new { message = "Instrutor removido com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuario nao encontrado" });
            }
        }
    }
}