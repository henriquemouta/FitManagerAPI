using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class SessaoCompletoVM
    {
        public int idSessao { get; set; }
        public string nomeSessao { get; set; } = string.Empty;
        public string? grupoMuscular { get; set; } = string.Empty;
        public List<ExercicioCompletoVM> exercicios { get; set; } = new List<ExercicioCompletoVM>();
    }
}