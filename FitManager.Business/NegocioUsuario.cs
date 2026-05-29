using FitManager.Models;
using FitManager.Repositories;
using FitManager.ViewModels;
using FitManager.ViewModels.Usuario;

namespace FitManager.Business
{
    public class NegocioUsuario : Negocio<Usuario>
    {
        private readonly RepositorioUsuario repositorio  ;

        public NegocioUsuario(RepositorioUsuario repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<UsuarioResponseVM> cadastrarAsync(CadastroUsuarioVM vm)
        {
            if (await repositorio.existeCpfAsync(vm.cpf))
                throw new InvalidOperationException("CPF ja cadastrado");

            if (await repositorio.existeEmailAsync(vm.email))
                throw new InvalidOperationException("Email ja cadastrado");

            if (await repositorio.existeMatriculaAsync(vm.matricula))
                throw new InvalidOperationException("Matricula ja cadastrada");

            var usuario = new Usuario
            {
                nomeCompleto = vm.nomeCompleto,
                cpf = vm.cpf,
                dataNascimento = vm.dataNascimento,
                email = vm.email,
                telefone = vm.telefone,
                matricula = vm.matricula,
                idCargo = vm.idCargo,
                senha = BCrypt.Net.BCrypt.HashPassword(vm.senha),
                createAt = DateTime.UtcNow
            };

            await repositorio.addAsync(usuario);
            return toResponseVM(usuario);
        }

        public async Task editarAsync(int id, EditarUsuarioVM vm)
        {
            var usuario = await repositorio.getByIdAsync(id.ToString())
                ?? throw new KeyNotFoundException("Usuario nao encontrado");

            if (vm.nomeCompleto != null) usuario.nomeCompleto = vm.nomeCompleto;
            if (vm.cpf != null) usuario.cpf = vm.cpf;
            if (vm.dataNascimento != null) usuario.dataNascimento = vm.dataNascimento.Value;
            if (vm.email != null) usuario.email = vm.email;
            if (vm.telefone != null) usuario.telefone = vm.telefone;

            await repositorio.updateAsync(id.ToString(), usuario);
        }

        public async Task<ListagemResponseVM<UsuarioResponseVM>> listarPorCargoAsync(
            int idCargo, string? search, int page, int limit)
        {
            var items = await repositorio.getByCargoAsync(idCargo, search, page, limit);
            var total = await repositorio.countByCargoAsync(idCargo);

            return new ListagemResponseVM<UsuarioResponseVM>
            {
                total = total,
                page = page,
                items = items.Select(toResponseVM)
            };
        }

        public async Task<int> contarPorCargoAsync(int idCargo)
            => await repositorio.countByCargoAsync(idCargo);

        public async Task<List<UsuarioResponseVM>> listarRecentesAsync(int idCargo, int limit)
        {
            var items = await repositorio.getByCargoAsync(idCargo, null, 1, limit);
            return items.Select(toResponseVM).ToList();
        }

        private UsuarioResponseVM toResponseVM(Usuario u) => new UsuarioResponseVM
        {
            id = u.idUsuario,
            nomeCompleto = u.nomeCompleto,
            cpf = u.cpf,
            dataNascimento = u.dataNascimento,
            email = u.email,
            telefone = u.telefone,
            matricula = u.matricula,
            cargoId = u.idCargo,
            cargo = u.idCargo == 1 ? "ADMIN" : u.idCargo == 2 ? "INSTRUTOR" : "ALUNO",
            createdAt = u.createAt
        };
    }
}