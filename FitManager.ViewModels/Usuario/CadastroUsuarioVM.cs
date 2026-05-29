using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels.Usuario
{
    public class CadastroUsuarioVM
    {
        public string nomeCompleto { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public DateOnly dataNascimento { get; set; }
        public string email { get; set; } = string.Empty;
        public string? telefone { get; set; }
        public string matricula { get; set; } = string.Empty;
        public int idCargo { get; set; }
        public string senha { get; set; } = string.Empty;
    }
}
