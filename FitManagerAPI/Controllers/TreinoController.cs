using FitManager.Business;
using FitManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/treinos")]
    public class TreinoController : ControllerBase
    {
        private readonly NegocioTreino _negocio;
        private readonly NegocioSessao _negocioSessao;

        public TreinoController(NegocioTreino negocio, NegocioSessao negocioSessao)
        {
            _negocio = negocio;
            _negocioSessao = negocioSessao;
        }

        [HttpGet]
        public async Task<IActionResult> listar(
            [FromQuery] string? search,
            [FromQuery] string? status,
            [FromQuery] int? alunoId,
            [FromQuery] int? instrutorId,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var resultado = await _negocio.listarTodosAsync(search, status, alunoId, instrutorId, page, limit);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCompleto(int id)
        {
            var treino = await _negocio.getCompletoAsync(id);
            if (treino == null) return NotFound(new { message = "Treino nao encontrado" });

            return Ok(new
            {
                id = treino.idTreino,
                nome = treino.nomeTreino,
                objetivo = treino.objetivo,
                observacoesGerais = treino.descricaoTreino,
                status = treino.statusTreino,
                aluno = new
                {
                    id = treino.usuarioTreinos.FirstOrDefault()?.aluno?.idUsuario,
                    nomeCompleto = treino.usuarioTreinos.FirstOrDefault()?.aluno?.nomeCompleto
                },
                instrutor = new
                {
                    id = treino.instrutor?.idUsuario,
                    nomeCompleto = treino.instrutor?.nomeCompleto
                },
                sessoes = treino.sessoes.Select(s => new
                {
                    id = s.idSessao,
                    nomeSessao = s.nomeSessao,
                    grupoMuscular = s.grupoMuscular,
                    exercicios = s.treinoExercicios.Select(te => new
                    {
                        id = te.id,
                        nomeExercicio = te.exercicio?.nomeExercicio,
                        series = te.series,
                        repeticoes = te.repeticoes,
                        carga = te.carga,
                        descanso = te.tempoDescanso,
                        observacoes = te.observacoes
                    })
                })
            });
        }

        [HttpGet("{treinoId}/sessoes")]
        public async Task<IActionResult> getSessoes(int treinoId)
        {
            var resultado = await _negocio.getSessoesDoTreinoAsync(treinoId);
            if (resultado == null)
                return NotFound (new { message = "Treino não encontrado" });
            
            return Ok(resultado);
        }

        [HttpGet("{id}/relatorio")]
        public async Task<IActionResult> getRelatorio(int id)
        {
            var relatorio = await _negocio.getRelatorioAsync(id);
            if (relatorio == null) return NotFound(new { message = "Treino nao encontrado" });
            return Ok(relatorio);
        }

        [HttpPost]
        public async Task<IActionResult> criar([FromBody] CriarTreinoVM vm)
        {
            try
            {
                var treino = await _negocio.criarAsync(vm);
                return StatusCode(201, new
                {
                    message = "Treino criado com sucesso",
                    treino = new { id = treino.idTreino, status = treino.statusTreino }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editar(int id, [FromBody] EditarTreinoVM vm)
        {
            try
            {
                await _negocio.editarAsync(id, vm);
                return Ok(new { message = "Treino atualizado com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Treino nao encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> excluir(int id)
        {
            try
            {
                await _negocio.deleteAsync(id);
                return Ok(new { message = "Treino removido com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Treino nao encontrado" });
            }
        }

        [HttpPost("{treinoId}/sessoes")]
        public async Task<IActionResult> criarSessao(int treinoId, [FromBody] CriarSessaoVM vm)
        {
            var sessao = await _negocioSessao.criarAsync(treinoId, vm);
            return StatusCode(201, new
            {
                message = "Sessao criada com sucesso",
                sessao = new { id = sessao.idSessao }
            });
        }
    }
}