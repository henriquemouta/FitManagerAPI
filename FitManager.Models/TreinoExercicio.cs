using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("treino_exercicio")]
    public class TreinoExercicio
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("id_sessao")]
        public int idSessao { get; set; }

        [Column("id_exercicio")]
        public int idExercicio { get; set; }

        [Column("series")]
        public int series { get; set; }

        [Column("carga")]
        public decimal carga { get; set; }

        [Column("tempo_descanso")]
        public int tempoDescanso { get; set; }

        [Column("observacoes")]
        public string? observacoes { get; set; }

        [Column("repeticoes")]
        public int repeticoes { get; set; }

        [ForeignKey("IdSessao")]
        public SessaoTreino? sessaoTreino { get; set; }

      
        public Exercicio? exercicio { get; set; }
    }
}