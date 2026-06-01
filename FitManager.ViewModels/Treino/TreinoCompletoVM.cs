using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class TreinoCompletoVM
    {
        public int treinoId { get; set; }
        public string nomeTreino { get; set; } = string.Empty;
        public string objetivo { get; set; } = string.Empty;
        public string observacoesGerais { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public PessoaResumoVM? aluno { get; set; } = new();
        public PessoaResumoVM? instrutor { get; set; } = new();
        public List<SessaoCompletoVM> sessoes { get; set; } = new List<SessaoCompletoVM>();
    }
}