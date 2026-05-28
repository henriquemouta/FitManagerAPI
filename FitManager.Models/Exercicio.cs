using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("exercicio")]
    public class Exercicio
    {
        [Key]
        [Column("id_exercicio")]
        public int idExercicio { get; set; }

        [Column("nome_exercicio")]
        public string nomeExercicio { get; set; } = string.Empty;

        [Column("descricao")]
        public string? descricao { get; set; }

        [Column("grupo_muscular")]
        public string? grupoMuscular { get; set; }

        [Column("observacoes")]
        public string? observacoes { get; set; }
    }
}