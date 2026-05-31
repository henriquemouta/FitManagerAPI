using FitManager.Models;
using FitManager.Repositories;
using FitManager.ViewModels;

namespace FitManager.Business
{
    public class NegocioTreino : Negocio<Treino>
    {
        private readonly RepositorioTreino _repo;
        private readonly RepositorioUsuario _repoUsuario;

        public NegocioTreino(RepositorioTreino repo, RepositorioUsuario repoUsuario) : base(repo)
        {
            _repo = repo;
            _repoUsuario = repoUsuario;
        }

        public async Task<Treino> criarAsync(CriarTreinoVM vm)
        {
            var aluno = await _repoUsuario.getByIdAsync(vm.alunoId)
                ?? throw new KeyNotFoundException("Usuario nao encontrado");

            var instrutor = await _repoUsuario.getByIdAsync(vm.instrutorId)
                ?? throw new KeyNotFoundException("Usuario nao encontrado");

            var treino = new Treino
            {
                nomeTreino = vm.nome,
                objetivo = vm.objetivo,
                descricaoTreino = vm.observacoesGerais,
                statusTreino = "RASCUNHO",
                idInstrutor = vm.instrutorId,
                dataCriacao = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _repo.addAsync(treino);

            var usuarioTreino = new UsuarioTreino
            {
                idTreino = treino.idTreino,
                idAluno = vm.alunoId,
                idInstrutor = vm.instrutorId,
                status = "ATIVO",
                dataAssociacao = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _repo.getContext().UsuarioTreinos.AddAsync(usuarioTreino);
            await _repo.getContext().SaveChangesAsync();

            return treino;
        }

        public async Task editarAsync(int id, EditarTreinoVM vm)
        {
            var treino = await _repo.getByIdAsync(id)
                ?? throw new KeyNotFoundException("Treino nao encontrado");

            if (vm.nome != null) treino.nomeTreino = vm.nome;
            if (vm.objetivo != null) treino.objetivo = vm.objetivo;
            if (vm.observacoesGerais != null) treino.descricaoTreino = vm.observacoesGerais;
            if (vm.status != null) treino.statusTreino = vm.status;

            await _repo.updateAsync(id, treino);
        }

        public async Task<Treino?> getCompletoAsync(int id)
            => await _repo.getCompletoAsync(id);
        
        public async Task<object?> getTreinoAtivoDoAlunoAsync(int alunoId)
        {
            var treino = await _repo.getAtivoByAlunoAsync(alunoId);
            if (treino == null) return null;

            var totalExercicios = treino.sessoes.Sum(s => s.treinoExercicios.Count);
            var tempoEstimadoMinutos = treino.tempoEstimado.HasValue
                ? treino.tempoEstimado.Value
                : treino.sessoes.Count * 25;
            
            return new
            {
                treinoId = treino.idTreino,
                nomeTreino = treino.nomeTreino,
                status = treino.statusTreino,
                instrutor = new
                {
                    id = treino.instrutor?.idUsuario,
                    nomeCompleto = treino.instrutor?.nomeCompleto
                },
                tempoEstimado = $"{tempoEstimadoMinutos} minutos",
                totalExercicios = totalExercicios,
                observacoesGerais = treino.descricaoTreino
            };
        }

        public async Task<object?> getSessoesDoTreinoAsync(int treinoId)
        {
            var treino = await _repo.getCompletoAsync(treinoId);
            if (treino == null) return null;

            return new
            {
                treinoId = treino.idTreino,
                sessoes = treino.sessoes.Select(s => new
                {
                    id = s.idSessao,
                    nomeSessao = s.nomeSessao,
                    grupoMuscular = s.grupoMuscular,
                    ordem = s.ordem,
                    totalExercicios = s.treinoExercicios.Count,
                    exercicios = s.treinoExercicios.Select(te => new
                    {
                        id = te.id,
                        nomeExercicio = te.exercicio?.nomeExercicio,
                        series = te.series,
                        repeticoes = te.repeticoes.ToString(),
                        carga = $"{te.carga} kg",
                        descanso = $"{te.tempoDescanso} s",
                        observacoes = te.observacoes,
                        ordem = te.ordem
                    })
                })
            };
        }

        public async Task<ListagemResponseVM<object>> listarTodosAsync(string? search, string? status, int? alunoId, int? instrutorId, int page, int limit)
        {
            var items = await _repo.getAllFilteredAsync(search, status, alunoId, instrutorId, page, limit);
            var total = await _repo.countAllFilteredAsync(search, status, alunoId, instrutorId);

            return new ListagemResponseVM<object>
            {
                total = total,
                page = page,
                items = items.Select(t => (object)new
                {
                    id = t.idTreino,
                    nomeTreino = t.nomeTreino,
                    aluno = t.usuarioTreinos.FirstOrDefault()?.aluno?.nomeCompleto ?? "",
                    instrutor = t.instrutor?.nomeCompleto ?? "",
                    status = t.statusTreino,
                    createdAt = t.dataCriacao
                })
            };
        }

        public async Task<ListagemResponseVM<object>> listarPorInstrutorAsync(int instrutorId, string? search, string? status, int? alunoId, int page, int limit)
        {
            var items = await _repo.getByInstrutorAsync(instrutorId, search, status, alunoId, page, limit);
            var total = await _repo.countByInstrutorAsync(instrutorId);

            return new ListagemResponseVM<object>
            {
                total = total,
                page = page,
                items = items.Select(t => (object)new
                {
                    id = t.idTreino,
                    nomeTreino = t.nomeTreino,
                    aluno = t.usuarioTreinos.FirstOrDefault()?.aluno?.nomeCompleto ?? "",
                    objetivo = t.objetivo,
                    status = t.statusTreino,
                    createdAt = t.dataCriacao
                })
            };
        }

        public async Task<List<object>> getAlunosByInstrutorAsync(int instrutorId)
        {
            var alunos = await _repo.getAlunosByInstrutorAsync(instrutorId);
            return alunos.Select(a => (object)new
            {
                id = a.idUsuario,
                nomeCompleto = a.nomeCompleto,
                matricula = a.matricula
            }).ToList();
        }

        public async Task<object?> getRelatorioAsync(int id)
        {
            var treino = await _repo.getCompletoAsync(id);
            if (treino == null) return null;

            var totalExercicios = treino.sessoes.Sum(s => s.treinoExercicios.Count);
            var tempoEstimado = treino.sessoes.Count * 30;

            return new
            {
                treinoId = treino.idTreino,
                nomeTreino = treino.nomeTreino,
                instrutor = treino.instrutor?.nomeCompleto ?? "",
                aluno = treino.usuarioTreinos.FirstOrDefault()?.aluno?.nomeCompleto ?? "",
                quantidadeSessoes = treino.sessoes.Count,
                quantidadeExercicios = totalExercicios,
                tempoEstimado = $"{tempoEstimado / 60}h{tempoEstimado % 60:D2}min",
                status = treino.statusTreino,
                createdAt = treino.dataCriacao
            };
        }
    }
}