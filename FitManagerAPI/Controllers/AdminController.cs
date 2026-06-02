using FitManager.Business;
using FitManager.ViewModels;
using FitManager.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/v1/dashboard")]
    public class AdminController : ControllerBase
    {
        private readonly NegocioUsuario negocio;
        private const int CARGO_ALUNO = 3;
        private const int CARGO_INSTRUTOR = 2;

        public AdminController(NegocioUsuario negocio) 
        { this.negocio = negocio; }
        [HttpGet("admin")]
        public async Task<IActionResult> dashboard()
        {
            var totalAlunos = await negocio.contarPorCargoAsync(CARGO_ALUNO);
            var totalInstrutores = await negocio.contarPorCargoAsync(CARGO_INSTRUTOR);
            var ultimosAlunos = await negocio.listarRecentesAsync(CARGO_ALUNO, 5);
            var ultimosInstrutores = await negocio.listarRecentesAsync(CARGO_INSTRUTOR, 5);

            return Ok(new DashboardAdminVM
            {
                totalAlunos = totalAlunos,
                totalInstrutores = totalInstrutores,
                ultimosAlunos = ultimosAlunos,
                ultimosInstrutores = ultimosInstrutores
            });
        }
    }
}