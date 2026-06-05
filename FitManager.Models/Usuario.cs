using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitManager.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int idUsuario { get; set; }

        [Column("nome_completo")]
        public string nomeCompleto { get; set; } = string.Empty;

        [Column("email")]
        public string email { get; set; } = string.Empty;

        [Column("senha")]
        public string senha { get; set; } = string.Empty;

        [Column("data_nascimento")]
        public DateOnly dataNascimento { get; set; }

        [Column("matricula")]
        public string matricula { get; set; } = string.Empty;

        [Column("create_at")]
        public DateTime createAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        [Column("update_at")]
        public DateTime? updateAt { get; set; }

        [Column("id_cargo")]
        public int idCargo { get; set; }

        [Column("cpf")]
        public string cpf { get; set; } = string.Empty;

        [Column("telefone")]
        public string? telefone { get; set; }

        [ForeignKey("idCargo")]
        public Cargo? cargo { get; set; }
    }
}