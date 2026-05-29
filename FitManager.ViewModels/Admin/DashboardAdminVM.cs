using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels.Admin
{
    public class DashboardAdminVM
    {
        public int totalAlunos { get; set; }
        public int totalInstrutores { get; set; }
        public IEnumerable<UsuarioResponseVM> ultimosAlunos { get; set; } = new List<UsuarioResponseVM>();
        public IEnumerable<UsuarioResponseVM> ultimosInstrutores { get; set; } = new List<UsuarioResponseVM>();
    }
}
