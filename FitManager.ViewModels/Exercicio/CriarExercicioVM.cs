using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class CriarExercicioVM
    {
        public int idExercicio { get; set; }
        public int series { get; set; }
        public int repeticoes { get; set; }
        public decimal carga { get; set; }
        public int descanso { get; set; }
        public string? observacoes { get; set; }
    }
}