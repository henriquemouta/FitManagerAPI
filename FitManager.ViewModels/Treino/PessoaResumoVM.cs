using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class PessoaResumoVM
    {
        public int? id { get; set; }
        public string? nomeCompleto { get; set; } = string.Empty;
        public string matricula { get; set; } = string.Empty;
    }
}