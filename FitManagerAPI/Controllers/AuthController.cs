using FitManager.Business;
using FitManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitManagerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly NegocioAuth _negocio;

        public AuthController(NegocioAuth negocio) { _negocio = negocio; }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginVM vm)
        {
            try
            {
                var resultado = await _negocio.loginAsync(vm);
                return Ok(resultado);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}