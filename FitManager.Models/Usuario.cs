using FitManager.Models.FitManager.Models;
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

        [Column("cpf")]
        public string cpf { get; set; } = string.Empty;

        [Column("create_at")]
        public DateTime createAt { get; set; } = DateTime.UtcNow;

        [Column("id_cargo")]
        public int idCargo { get; set; }

        [ForeignKey("idCargo")]
        public Cargo? cargo { get; set; }
    }
}