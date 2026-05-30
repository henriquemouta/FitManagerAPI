using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class CriarTreinoVM
    {
        public int alunoId { get; set; }
        public int instrutorId { get; set; }
        public string nome { get; set; } = string.Empty;
        public string? objetivo { get; set; }
        public string? observacoesGerais { get; set; }
    }
}