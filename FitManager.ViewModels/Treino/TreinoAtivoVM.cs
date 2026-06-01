using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class TreinoAtivoVM
    {
        public int treinoId { get; set; }
        public string nomeTreino { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public PessoaResumoVM? instrutor { get; set; } = new();
        public string tempoEstimado { get; set; } = string.Empty;
        public int totalExercicios { get; set; }
        public string? observacoesGerais { get; set; } = string.Empty;
    }
}