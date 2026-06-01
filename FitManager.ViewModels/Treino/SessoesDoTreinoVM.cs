using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class SessoesDoTreinoVM
    {
        public int treinoId { get; set; }
        public List<SessaoCompletoVM> sessoes { get; set; } = new();
    }
}