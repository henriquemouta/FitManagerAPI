using FitManager.Models;
using FitManager.Repositories;
using FitManager.ViewModels;

namespace FitManager.Business
{
    public class NegocioSessao : Negocio<SessaoTreino>
    {
        private readonly RepositorioSessao _repo;

        public NegocioSessao(RepositorioSessao repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<SessaoTreino> criarAsync(int treinoId, CriarSessaoVM vm)
        {
            var sessao = new SessaoTreino
            {
                idTreino = treinoId,
                nomeSessao = vm.nomeSessao,
                grupoMuscular = vm.grupoMuscular,
                ordem = vm.ordem
            };

            await _repo.addAsync(sessao);
            return sessao;
        }

        public async Task editarAsync(int id, EditarSessaoVM vm)
        {
            var sessao = await _repo.getByIdAsync(id)
                ?? throw new KeyNotFoundException("Sessao nao encontrada");

            if (vm.nomeSessao != null) sessao.nomeSessao = vm.nomeSessao;
            if (vm.grupoMuscular != null) sessao.grupoMuscular = vm.grupoMuscular;
            if (vm.ordem.HasValue) sessao.ordem = vm.ordem.Value;
            await _repo.updateAsync(id, sessao);
        }
    }
}