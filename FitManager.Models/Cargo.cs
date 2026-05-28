using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


  

    namespace FitManager.Models
    {
        [Table("cargo")]
        public class Cargo
        {
            [Key]
            [Column("id_cargo")]
            public int idCargo { get; set; }

            [Column("nome_cargo")]
            public string nomeCargo { get; set; } = string.Empty;

            public ICollection<Usuario> usuarios { get; set; } = new List<Usuario>();
        }
    }

