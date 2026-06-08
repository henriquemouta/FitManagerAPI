using FitManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FitManager.Repositories
{
    public class RepositorioTreino : BaseRepositorio<Treino>
    {
        public RepositorioTreino(AppDbContext context) : base(context) { }

        public async Task<Treino?> getAtivoByAlunoAsync(int alunoId)
            => await banco
                .Include(t => t.instrutor)
                .Include(t => t.sessoes)
                    .ThenInclude(s => s.treinoExercicios)
                .Include (t => t.usuarioTreinos)
                .FirstOrDefaultAsync(t =>
                    t.usuarioTreinos.Any(ut => ut.idAluno == alunoId));

        public async Task<List<Treino>> getByInstrutorAsync(int instrutorId, string? search, string? status, int? alunoId, int page, int limit)
        {
            var query = banco
                .Include(t => t.instrutor)
                .Include(t => t.sessoes)
                .Include(t => t.usuarioTreinos)
                    .ThenInclude(ut => ut.aluno)
                .Where(t => t.id_instrutor == instrutorId);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.nomeTreino.Contains(search));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.statusTreino == status);

            if (alunoId.HasValue)
                query = query.Where(t => t.usuarioTreinos.Any(ut => ut.idAluno == alunoId));

            return await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Treino>> getAllFilteredAsync(string? search, string? status, int? alunoId, int? instrutorId, int page, int limit)
        {
            var query = banco
                .Include(t => t.instrutor)
                .Include(t => t.sessoes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.nomeTreino.Contains(search));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.statusTreino == status);

            if (alunoId.HasValue)
                query = query.Where(t => t.usuarioTreinos.Any(ut => ut.idAluno == alunoId));

            if (instrutorId.HasValue)
                query = query.Where(t => t.id_instrutor == instrutorId);

            return await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Treino?> getCompletoAsync(int id)
            => await banco
         .Include(t => t.instrutor)
                .Include(t => t.sessoes)
                    .ThenInclude(s => s.treinoExercicios)
                        .ThenInclude(te => te.exercicio)
                .Include(t => t.usuarioTreinos)
                    .ThenInclude(ut => ut.aluno)
                .FirstOrDefaultAsync(t => t.id_treino == id);
        public async Task<List<Usuario>> getAlunosByInstrutorAsync(int instrutorId)
            => await _context.UsuarioTreinos
                .Where(ut => ut.idInstrutor == instrutorId)
                .Select(ut => ut.aluno!)
                .Distinct()
                .ToListAsync();

        public async Task<int> countByInstrutorAsync(int instrutorId)
            => await banco.CountAsync(t => t.id_instrutor  == instrutorId);

        public async Task<int> countAllFilteredAsync(string? search, string? status, int? alunoId, int? instrutorId)
        {
            var query = banco.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.nomeTreino.Contains(search));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.statusTreino == status);

            if (alunoId.HasValue)
                query = query.Where(t => t.usuarioTreinos.Any(ut => ut.idAluno == alunoId));

            if (instrutorId.HasValue)
                query = query.Where(t => t.id_instrutor == instrutorId);

            return await query.CountAsync();
        }
    }
}