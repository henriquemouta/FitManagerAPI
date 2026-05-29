using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class UsuarioResponseVM
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public DateOnly dataNascimento { get; set; }
        public string email { get; set; } = string.Empty;
        public string? telefone { get; set; }
        public string matricula { get; set; } = string.Empty;
        public int cargoId { get; set; }
        public string cargo { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
    }
}