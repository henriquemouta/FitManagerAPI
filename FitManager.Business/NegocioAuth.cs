using FitManager.Repositories;
using FitManager.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitManager.Business
{
    public class NegocioAuth
    {
        private readonly RepositorioUsuario _repo;
        private readonly IConfiguration _config;

        public NegocioAuth(RepositorioUsuario repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<object> loginAsync(LoginVM vm)
        {
            var usuario = await _repo.getByEmailAsync(vm.email) 
                ?? throw new UnauthorizedAccessException("Email ou senha invalidos");

            var senhaValida = BCrypt.Net.BCrypt.Verify(vm.senha, usuario.senha);

        
            if (!senhaValida)
                throw new UnauthorizedAccessException("Email ou senha invalidos");

            var perfil = usuario.idCargo switch
            {
                1 => "ADMIN",
                2 => "INSTRUTOR",
                3 => "ALUNO",
                _ => "ALUNO"
            };

            var token = gerarToken(usuario.idUsuario, usuario.email, perfil);

            return new
            {
                token,
                perfil,
                id = usuario.idUsuario,
                nomeCompleto = usuario.nomeCompleto
            };
        }

        private string gerarToken(int id, string email, string perfil)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, perfil)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    double.Parse(_config["Jwt:ExpiresInHours"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}