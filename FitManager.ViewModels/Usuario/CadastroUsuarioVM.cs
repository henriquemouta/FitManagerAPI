using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FitManager.ViewModels.Usuario
{
    public class CadastroUsuarioVM
    {
        public string nomeCompleto { get; set; } = string.Empty;
        
        [Required] [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$")]
        public string cpf { get; set; } = string.Empty;
        public DateOnly dataNascimento { get; set; }
        
        [Required] [EmailAddress]
        public string email { get; set; } = string.Empty;
        public string? telefone { get; set; }
        public string matricula { get; set; } = string.Empty;
        
        [Required] [Range(1, int.MaxValue)]
        public int idCargo { get; set; }
        
        [Required] [MinLength(6)]
        public string senha { get; set; } = string.Empty;
    }
}
