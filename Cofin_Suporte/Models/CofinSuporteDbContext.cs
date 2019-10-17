using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Cofin_Suporte.Models
{
    public partial class CofinSuporteDbContext : DbContext
    {
        public CofinSuporteDbContext()
        {
        }

        public CofinSuporteDbContext(DbContextOptions<CofinSuporteDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Backlog> Backlog { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cofin> Cofin { get; set; }
        public virtual DbSet<CofinSolucao> CofinSolucao { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Solucao> Solucao { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=xxx;Initial Catalog=RegisterBugs;Persist Security Info=True;User ID=xxx;Password=xxx;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Backlog>(entity =>
            {
                entity.HasKey(e => e.IdBacklog)
                    .HasName("PK_dbo.Backlog");

                entity.Property(e => e.DescricaoBacklog)
                    .IsRequired()
                    .HasColumnName("Descricao_Backlog")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK_dbo.Categoria");

                entity.Property(e => e.DescricaoCategoria)
                    .IsRequired()
                    .HasColumnName("Descricao_Categoria")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Cofin>(entity =>
            {
                entity.HasKey(e => e.IdCofin)
                    .HasName("PK_dbo.Cofin");

                entity.HasIndex(e => e.IdBacklog)
                    .HasName("IX_IdBacklog");

                entity.HasIndex(e => e.IdCategoria)
                    .HasName("IX_IdCategoria");

                entity.HasIndex(e => e.IdTipo)
                    .HasName("IX_IdTipo");

                entity.Property(e => e.Assunto).IsRequired();

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Descricao).IsRequired();

                entity.Property(e => e.Observacao).IsRequired();

                entity.HasOne(d => d.IdBacklogNavigation)
                    .WithMany(p => p.Cofin)
                    .HasForeignKey(d => d.IdBacklog)
                    .HasConstraintName("FK_dbo.Cofin_dbo.Backlog_IdBacklog");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Cofin)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_dbo.Cofin_dbo.Categoria_IdCategoria");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Cofin)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_dbo.Cofin_dbo.Tipo_IdTipo");
            });

            modelBuilder.Entity<CofinSolucao>(entity =>
            {
                entity.HasKey(e => new { e.IdCofin, e.IdSolucao })
                    .HasName("PK_dbo.CofinSolucao");

                entity.HasOne(d => d.IdCofinNavigation)
                    .WithMany(p => p.CofinSolucao)
                    .HasForeignKey(d => d.IdCofin)
                    .HasConstraintName("FK_dbo.CofinSolucao_dbo.Cofin_IdCofin");

                entity.HasOne(d => d.IdSolucaoNavigation)
                    .WithMany(p => p.CofinSolucao)
                    .HasForeignKey(d => d.IdSolucao)
                    .HasConstraintName("FK_dbo.CofinSolucao_dbo.Solucao_IdSolucao");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Solucao>(entity =>
            {
                entity.HasKey(e => e.IdSolucao)
                    .HasName("PK_dbo.Solucao");

                entity.Property(e => e.DescricaoSolução)
                    .IsRequired()
                    .HasColumnName("Descricao_Solução")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK_dbo.Tipo");

                entity.Property(e => e.DescricaoTipo)
                    .IsRequired()
                    .HasColumnName("Descricao_Tipo")
                    .HasMaxLength(100);
            });
        }
    }
}
