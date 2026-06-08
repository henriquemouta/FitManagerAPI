using FitManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitManager.Repositories
{
    public class RepositorioSessao : BaseRepositorio<SessaoTreino>
    {
        public RepositorioSessao(AppDbContext context) : base(context) { }

        public async Task<List<SessaoTreino>> getByTreinoAsync(int treinoId)
            => await banco
                .Include(s => s.treinoExercicios)
                    .ThenInclude(te => te.exercicio)
                .Where(s => s.idTreino == treinoId)
                .ToListAsync();
        
        public async Task<bool> treinoExisteAsync(int treinoId)
            => await banco.AnyAsync(s => s.idTreino == treinoId);
    }
}