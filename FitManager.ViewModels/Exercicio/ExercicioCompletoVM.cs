using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class ExercicioCompletoVM
    {
        public int idExercicio { get; set; }
        public string? nomeExercicio { get; set; } = string.Empty;
        public int? series { get; set; }
        public int repeticoes { get; set; }
        public decimal carga { get; set; }
        public string? descanso { get; set; }
        public string? observacoes { get; set; }
        public int ordem { get; set; }
    }
}