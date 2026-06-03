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

            modelBuilder.Entity<Treino>(entity =>
            {
                entity.ToTable("treino");
                entity.HasKey(t => t.id_treino);

                entity.Property(t => t.id_treino).HasColumnName("id_treino");
                entity.Property(t => t.nomeTreino).HasColumnName("nome_treino");
                entity.Property(t => t.descricaoTreino).HasColumnName("descricao_treino");
                entity.Property(t => t.tempoEstimado).HasColumnName("tempo_estimado");
                entity.Property(t => t.statusTreino).HasColumnName("status_treino");
                entity.Property(t => t.objetivo).HasColumnName("objetivo");
                entity.Property(t => t.dataCriacao).HasColumnName("data_criacao");
                entity.Property(t => t.id_instrutor).HasColumnName("id_instrutor");

                entity.HasOne(t => t.instrutor)
                    .WithMany()
                    .HasForeignKey(t => t.id_instrutor);
            });

            modelBuilder.Entity<SessaoTreino>(entity =>
            {
                entity.ToTable("sessao_treino");
                entity.HasKey(s => s.idSessao);

                entity.Property(s => s.idSessao).HasColumnName("id_sessao");
                entity.Property(s => s.nomeSessao).HasColumnName("nome_sessao");
                entity.Property(s => s.grupoMuscular).HasColumnName("grupo_muscular");
                entity.Property(s => s.idTreino).HasColumnName("id_treino");

                entity.HasOne(s => s.treino)
                    .WithMany(t => t.sessoes)
                    .HasForeignKey(s => s.idTreino);
            });

            modelBuilder.Entity<TreinoExercicio>(entity =>
            {
                entity.ToTable("treino_exercicio");
                entity.HasKey(te => te.id);

                entity.Property(te => te.id).HasColumnName("id");
                entity.Property(te => te.idSessao).HasColumnName("id_sessao");
                entity.Property(te => te.idExercicio).HasColumnName("id_exercicio");
                entity.Property(te => te.series).HasColumnName("series");
                entity.Property(te => te.repeticoes).HasColumnName("repeticoes");
                entity.Property(te => te.carga).HasColumnName("carga");
                entity.Property(te => te.tempoDescanso).HasColumnName("tempo_descanso");
                entity.Property(te => te.observacoes).HasColumnName("observacoes");

                entity.HasOne(te => te.sessaoTreino)
                    .WithMany(s => s.treinoExercicios)
                    .HasForeignKey(te => te.idSessao);

                entity.HasOne(te => te.exercicio)
                    .WithMany()
                    .HasForeignKey(te => te.idExercicio);
            });

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