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

        public int idTreino { get; set; }
        public int idAluno { get; set; }
        public int idInstrutor { get; set; }

        public string? status { get; set; }
        public DateOnly dataAssociacao { get; set; }

        public Usuario? aluno { get; set; }
        public Usuario? instrutor { get; set; }
        public Treino? treino { get; set; }
    }
}