using FitManager.Business;
using FitManager.ViewModels;
using FitManager.ViewModels.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
#if !DEBUG
    [Authorize(Roles = "ADMIN")]
#endif
    [ApiController]
    [Route("api/v1/alunos")]
    public class AlunoController : ControllerBase
    {
        private readonly NegocioUsuario negocio;
        private readonly NegocioTreino negocioTreino;
        private const int CARGO_ALUNO = 3;

        public AlunoController(NegocioUsuario negocio, NegocioTreino negocioTreino) { this.negocio = negocio; this.negocioTreino = negocioTreino; }

        [HttpGet("{alunoId}/treino")]
        public async Task<IActionResult> getTreinoAtivo(int alunoId)
        {
            var treino = await negocioTreino.getTreinoAtivoDoAlunoAsync(alunoId);
            if (treino == null)
                return NotFound(new { message = "Nenhum treino ativo encontrado para este aluno" });
            
            return Ok(treino);
        }

        [HttpGet]
        public async Task<IActionResult> listar(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var resultado = await negocio.listarPorCargoAsync(CARGO_ALUNO, search, page, limit);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> cadastrar([FromBody] CadastroUsuarioVM vm)
        {
            try
            {
                var aluno = await negocio.cadastrarAsync(vm);
                return StatusCode(201, new
                {
                    message = "Aluno cadastrado com sucesso",
                    aluno = new { id = aluno.id, cargoId = aluno.cargoId, cargo = aluno.cargo }
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
                return Ok(new { message = "Aluno atualizado com sucesso" });
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
                await negocio.deleteAsync(id);
                return Ok(new { message = "Aluno removido com sucesso" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuario nao encontrado" });
            }
        }
    }
}