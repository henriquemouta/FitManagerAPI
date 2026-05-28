using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("usuario_treino")]
    public class UsuarioTreino
    {
        [Key]
        [Column("id_treino_associacao")]
        public int idTreinoAssociacao { get; set; }

        [Column("id_instrutor")]
        public int idInstrutor { get; set; }

        [Column("id_aluno")]
        public int idAluno { get; set; }

        [Column("id_treino")]
        public int idTreino { get; set; }

        [Column("status")]
        public string? status { get; set; }

        [Column("data_associacao")]
        public DateOnly dataAssociacao { get; set; }

        [ForeignKey("IdInstrutor")]
        public Usuario? instrutor { get; set; }

        [ForeignKey("IdAluno")]
        public Usuario? aluno { get; set; }

        [ForeignKey("IdTreino")]
        public Treino? treino { get; set; }
    }
}