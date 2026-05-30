using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FitManager.ViewModels
{
    public class EditarUsuarioVM
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome completo deve conter entre 3 e 100 caracteres.")]
        public string? nomeCompleto { get; set; }
        public string? cpf { get; set; }
        public DateOnly? dataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? email { get; set; }

        [RegularExpression(@"^\d{10,11}$",
         ErrorMessage = "O telefone deve conter apenas números e ter entre 10 e 11 dígitos.")]
        public string? telefone { get; set; }
    }
}
