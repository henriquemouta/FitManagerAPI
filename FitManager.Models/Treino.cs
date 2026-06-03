using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("treino")]
    public class Treino
    {
        [Key]
        [Column("id_treino")]
        public int id_treino { get; set; }

        [Column("nome_treino")]
        public string nomeTreino { get; set; } = string.Empty;

        [Column("descricao_treino")]
        public string? descricaoTreino { get; set; }

        [Column("tempo_estimado")]
        public int? tempoEstimado { get; set; }

        [Column("status_treino")]
        public string statusTreino { get; set; } = "Ativo";

        [Column("objetivo")]
        public string? objetivo { get; set; }

        [Column("data_criacao")]
        public DateOnly dataCriacao { get; set; }

        [Column("id_instrutor")]
        public int id_instrutor { get; set; }

  
        public Usuario? instrutor { get; set; }

        public ICollection<UsuarioTreino> usuarioTreinos { get; set; } = new List<UsuarioTreino>();

        public ICollection<SessaoTreino> sessoes { get; set; } = new List<SessaoTreino>();
    }
}