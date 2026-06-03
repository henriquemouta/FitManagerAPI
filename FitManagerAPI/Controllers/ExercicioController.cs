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

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var exercicio = await _negocio.getByIdAsync(id);
            if (exercicio == null) return NotFound(new { message = "Exercicio nao encontrado" });

            return Ok(new
            {
                id = exercicio.id,
                nomeExercicio = exercicio.exercicio?.nomeExercicio,
                series = exercicio.series,
                repeticoes = exercicio.repeticoes,
                carga = exercicio.carga,
                descanso = exercicio.tempoDescanso,
                observacoes = exercicio.observacoes
            });
        }

        [HttpGet("sessao/{sessaoId}")]
        public async Task<IActionResult> getBySessao(int sessaoId)
        {
            var exercicios = await _negocio.getBySessaoAsync(sessaoId);

            return Ok(new
            {
                items = exercicios.Select(te => new
                {
                    id = te.id,
                    nomeExercicio = te.exercicio?.nomeExercicio,
                    series = te.series,
                    repeticoes = te.repeticoes,
                    carga = te.carga,
                    descanso = te.tempoDescanso,
                    observacoes = te.observacoes
                })
            });
        }

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