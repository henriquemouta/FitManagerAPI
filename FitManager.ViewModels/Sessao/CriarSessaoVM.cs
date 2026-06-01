using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class CriarSessaoVM
    {
        public string nomeSessao { get; set; } = string.Empty;
        public string? grupoMuscular { get; set; }
        public int ordem { get; set; } = 1;
    }
}
