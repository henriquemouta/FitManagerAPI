using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class EditarUsuarioVM
    {
        public string? nomeCompleto { get; set; }
        public string? cpf { get; set; }
        public DateOnly? dataNascimento { get; set; }
        public string? email { get; set; }
        public string? telefone { get; set; }
    }
}
