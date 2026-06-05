using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string senha { get; set; } = string.Empty;
    }
}