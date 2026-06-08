using FitManager.Models;
using FitManager.Repositories;
using FitManager.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FitManager.Business
{
    public class NegocioExercicio : Negocio<TreinoExercicio>
    {
        private readonly RepositorioExercicio repo;

        public NegocioExercicio(RepositorioExercicio repo) : base(repo)
        {
            this.repo = repo;
        }

        public async Task<TreinoExercicio> criarAsync(int sessaoId, CriarExercicioVM vm)
        {
            try
            {

                var sessao = await repo.getContext().SessoesTreino
                    .FirstOrDefaultAsync(s => s.idSessao == sessaoId)
                    ?? throw new KeyNotFoundException($"Sessao {sessaoId} nao encontrada");

                var treinoExercicio = new TreinoExercicio
                {
                    idSessao = sessaoId,
                    idExercicio = vm.idExercicio,
                    series = vm.series,
                    repeticoes = vm.repeticoes,
                    carga = vm.carga,
                    tempoDescanso = vm.descanso,
                    observacoes = vm.observacoes
                };

                await repo.addAsync(treinoExercicio);
                return treinoExercicio;
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao criar exercício: {ex.Message}", ex);
            }
        }

        public async Task editarAsync(int id, EditarExercicioVM vm)
        {
            try
            {
                var te = await repo.getByIdAsync(id)
                    ?? throw new KeyNotFoundException("Exercicio nao encontrado");

                if (vm.series.HasValue) te.series = vm.series.Value;
                if (vm.repeticoes.HasValue) te.repeticoes = vm.repeticoes.Value;
                if (vm.carga.HasValue) te.carga = vm.carga.Value;
                if (vm.descanso.HasValue) te.tempoDescanso = vm.descanso.Value;
                if (vm.observacoes != null) te.observacoes = vm.observacoes;

                await repo.updateAsync(id, te);
            }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao editar exercicio: {ex.Message}", ex);
            }
        }

        public async Task<List<TreinoExercicio>> getBySessaoAsync(int sessaoId)
                    => await repo.getBySessaoAsync(sessaoId);


    }
}