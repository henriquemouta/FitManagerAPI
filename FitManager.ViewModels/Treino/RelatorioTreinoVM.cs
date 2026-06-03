using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class RelatorioTreinoVM
    {
        public int treinoId { get; set; }
        public string nomeTreino { get; set; } = string.Empty;
        public string instrutor { get; set; } = string.Empty;
        public string aluno { get; set; } = string.Empty;
        public int quantidadeSessoes { get; set; }
        public int quantidadeExercicios { get; set; }
        public string tempoEstimado { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateOnly createdAt { get; set; }
    }
}