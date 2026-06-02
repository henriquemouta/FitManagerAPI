using FitManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitManager.Repositories
{
    public class RepositorioUsuario : BaseRepositorio<Usuario>
    {
        public RepositorioUsuario(AppDbContext context) : base(context) { }


        public async Task<bool> existeCpfAsync(string cpf)
            => await banco.AnyAsync(u => u.cpf == cpf);

        public async Task<bool> existeEmailAsync(string email)
            => await banco.AnyAsync(u => u.email == email);

        public async Task<bool> existeMatriculaAsync(string matricula)
            => await banco.AnyAsync(u => u.matricula == matricula);

        public async Task<Usuario?> getByEmailAsync(string email)
    => await banco.FirstOrDefaultAsync(u => u.email == email);

        public async Task<List<Usuario>> getByCargoAsync(int idCargo, string? search, int page, int limit)
        {
            var query = banco
                .Include(u => u.cargo)
                .Where(u => u.idCargo == idCargo);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u =>
                    u.nomeCompleto.Contains(search) ||
                    u.matricula.Contains(search));

            return await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> countByCargoAsync(int idCargo, string? search) 
        {
            var query = banco.Where(u => u.idCargo == idCargo);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u =>
                    u.nomeCompleto.Contains(search) ||
                    u.matricula.Contains(search));

            return await query.CountAsync();
        }

        public async Task<Usuario?> getByCargoAndIdAsync(int id, int idCargo)
        
            => await banco
                .Include(u => u.cargo)
                .FirstOrDefaultAsync(u => u.idUsuario == id && u.idCargo == idCargo);
    }
}