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
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string nomeCompleto { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$",
            ErrorMessage = "CPF deve estar no formato 000.000.000-00 ou 00000000000.")]
        public string cpf { get; set; } = string.Empty;

        [Required]
        public DateOnly dataNascimento { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string email { get; set; } = string.Empty;

        [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$",
            ErrorMessage = "Telefone deve estar no formato (00) 0000-0000 ou (00) 00000-0000.")]
        public string? telefone { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        public string matricula { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cargo inválido.")]
        public int idCargo { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string senha { get; set; } = string.Empty;
    }
}
