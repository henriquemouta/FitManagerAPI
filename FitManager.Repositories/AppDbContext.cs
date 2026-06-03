using Microsoft.EntityFrameworkCore;
using FitManager.Models;


namespace FitManager.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<SessaoTreino> SessoesTreino { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
        public DbSet<UsuarioTreino> UsuarioTreinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento Usuario -> Cargo
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.cargo)
                .WithMany(c => c.usuarios)
                .HasForeignKey(u => u.idCargo);

            // Relacionamento Treino -> Instrutor
            modelBuilder.Entity<Treino>()
                .HasOne(t => t.instrutor)
                .WithMany()
                .HasForeignKey(t => t.id_instrutor);

            // Relacionamento SessaoTreino -> Treino
            modelBuilder.Entity<SessaoTreino>()
                .HasOne(s => s.treino)
                .WithMany(t => t.sessoes)
                .HasForeignKey(s => s.idTreino);

            // Relacionamento TreinoExercicio -> SessaoTreino
            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.sessaoTreino)
                .WithMany(s => s.treinoExercicios)
                .HasForeignKey(te => te.idSessao);

            // Relacionamento TreinoExercicio -> Exercicio
            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.exercicio)
                .WithMany()
                .HasForeignKey(te => te.idExercicio);

            // Relacionamentos UsuarioTreino
            modelBuilder.Entity<UsuarioTreino>(entity =>
            {
                entity.ToTable("usuario_treino");
                entity.HasKey(ut => ut.idTreinoAssociacao);

                entity.Property(ut => ut.idTreino).HasColumnName("id_treino");
                entity.Property(ut => ut.idAluno).HasColumnName("id_aluno");
                entity.Property(ut => ut.idInstrutor).HasColumnName("id_instrutor");
                entity.Property(ut => ut.status).HasColumnName("status");
                entity.Property(ut => ut.dataAssociacao).HasColumnName("data_associacao");

                entity.HasOne(ut => ut.treino)
                    .WithMany(t => t.usuarioTreinos)
                    .HasForeignKey(ut => ut.idTreino);

                entity.HasOne(ut => ut.aluno)
                    .WithMany()
                    .HasForeignKey(ut => ut.idAluno)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ut => ut.instrutor)
                    .WithMany()
                    .HasForeignKey(ut => ut.idInstrutor)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}