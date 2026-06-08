using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class TreinoItemVM
    {
        public int treinoId { get; set; }
        public string nomeTreino { get; set; } = string.Empty;
        public int? alunoId { get; set; }
        public string aluno { get; set; } = string.Empty;
        public string instrutor { get; set; } = string.Empty;
        public string? objetivo { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public DateOnly createdAt { get; set; }
    }
}