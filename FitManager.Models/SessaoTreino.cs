using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("sessao_treino")]
    public class SessaoTreino
    {
        [Key]
        [Column("id_sessao")]
        public int idSessao { get; set; }

        [Column("nome_sessao")]
        public string nomeSessao { get; set; } = string.Empty;

        [Column("grupo_muscular")]
        public string? grupoMuscular { get; set; }

        [Column("ordem")]
        public int ordem { get; set; } = 1;

        [Column("id_treino")]
        public int idTreino { get; set; }

        [ForeignKey("idTreino")]
        public Treino? treino { get; set; }

        public ICollection<TreinoExercicio> treinoExercicios { get; set; } = new List<TreinoExercicio>();
    }
}