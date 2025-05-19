using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Senai_Notas.Models;

namespace Senai_Notas.Context;

public partial class ProjetoSenaiContext : DbContext
{
    public ProjetoSenaiContext()
    {
    }

    public ProjetoSenaiContext(DbContextOptions<ProjetoSenaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anexo> Anexos { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<NotaTag> NotaTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    string connectionString = Environment.GetEnvironmentVariable("connection_string");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anexo>(entity =>
        {
            entity.HasKey(e => e.IdAnexo).HasName("PK__Anexo__A3A70BAAEEE10232");

            entity.ToTable("Anexo");

            entity.Property(e => e.IdAnexo).HasColumnName("idAnexo");
            entity.Property(e => e.DataUpload)
                .HasColumnType("datetime")
                .HasColumnName("dataUpload");
            entity.Property(e => e.NomeArquivo)
                .IsUnicode(false)
                .HasColumnName("nomeArquivo");
            entity.Property(e => e.Url)
                .HasMaxLength(1)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Notas__AD5F462E3249ECE0");

            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.Conteudo).HasColumnType("text");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("dataCriacao");
            entity.Property(e => e.IdAnexo).HasColumnName("idAnexo");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UltimoRefresh)
                .HasColumnType("datetime")
                .HasColumnName("ultimoRefresh");

            entity.HasOne(d => d.IdAnexoNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdAnexo)
                .HasConstraintName("FK__Notas__idAnexo__7E37BEF6");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Notas__idUsuario__7D439ABD");
        });

        modelBuilder.Entity<NotaTag>(entity =>
        {
            entity.HasKey(e => e.IdNotaTag).HasName("PK__NotaTag__97932B08A00A25D7");

            entity.ToTable("NotaTag");

            entity.Property(e => e.IdNotaTag).HasColumnName("idNotaTag");
            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.IdTag).HasColumnName("idTag");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.NotaTags)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__NotaTag__idNota__01142BA1");

            entity.HasOne(d => d.IdTagNavigation).WithMany(p => p.NotaTags)
                .HasForeignKey(d => d.IdTag)
                .HasConstraintName("FK__NotaTag__idTag__02084FDA");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("PK__TAG__020FEDB881B7F6F7");

            entity.ToTable("TAG");

            entity.Property(e => e.IdTag).HasColumnName("idTag");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6A34E83AC");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(100)
                .HasColumnName("urlFoto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
