using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitManager.ViewModels
{
    public class ListagemResponseVM<T>
    {
        public int total { get; set; }
        public int page { get; set; }
        public IEnumerable<T> items { get; set; } = new List<T>();
    }
}