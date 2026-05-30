using FitManager.Business;
using FitManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/exercicios")]
    public class ExercicioController : ControllerBase
    {
        private readonly NegocioExercicio _negocio;

        public ExercicioController(NegocioExercicio negocio) { _negocio = negocio; }

        [HttpPut("{id}")]
        public async Task<IActionResult> editar(int id, [FromBody] EditarExercicioVM vm)
        {
            try
            {
                await _negocio.editarAsync(id, vm);
                return Ok(new { message = "Exercicio atualizado com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Exercicio nao encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> excluir(int id)
        {
            await _negocio.deleteAsync(id);
            return Ok(new { message = "Exercicio removido com sucesso" });
        }
    }
}