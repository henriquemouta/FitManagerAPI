using FitManager.Business;
using FitManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/sessoes")]
    public class SessaoController : ControllerBase
    {
        private readonly NegocioSessao _negocio;
        private readonly NegocioExercicio _negocioExercicio;

        public SessaoController(NegocioSessao negocio, NegocioExercicio negocioExercicio)
        {
            _negocio = negocio;
            _negocioExercicio = negocioExercicio;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editar(int id, [FromBody] EditarSessaoVM vm)
        {
            try
            {
                await _negocio.editarAsync(id, vm);
                return Ok(new { message = "Sessao atualizada com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Sessao nao encontrada" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> excluir(int id)
        {
            await _negocio.deleteAsync(id);
            return Ok(new { message = "Sessao removida com sucesso" });
        }

        [HttpPost("{sessaoId}/exercicios")]
        public async Task<IActionResult> criarExercicio(int sessaoId, [FromBody] CriarExercicioVM vm)
        {
            var exercicio = await _negocioExercicio.criarAsync(sessaoId, vm);
            return StatusCode(201, new
            {
                message = "Exercicio adicionado com sucesso",
                exercicio = new { id = exercicio.id }
            });
        }
    }
}