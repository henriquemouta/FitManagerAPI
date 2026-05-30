using FitManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitManager.Repositories
{
    public class RepositorioExercicio : BaseRepositorio<TreinoExercicio>
    {
        public RepositorioExercicio(AppDbContext context) : base(context) { }

        public AppDbContext getContext() => _context;

        public async Task<List<TreinoExercicio>> getBySessaoAsync(int sessaoId)
            => await banco
                .Include(te => te.exercicio)
                .Where(te => te.idSessao == sessaoId)
                .ToListAsync();
    }
}