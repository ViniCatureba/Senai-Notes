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

    public virtual DbSet<HistoricoNotum> HistoricoNota { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<NotaTag> NotaTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anexo>(entity =>
        {
            entity.HasKey(e => e.IdAnexo).HasName("PK__Anexo__A3A70BAA11C0B2C2");

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

        modelBuilder.Entity<HistoricoNotum>(entity =>
        {
            entity.HasKey(e => e.IdHistoNota).HasName("PK__Historic__0B8F1AB7B8805147");

            entity.Property(e => e.IdHistoNota).HasColumnName("idHistoNota");
            entity.Property(e => e.ConteudoAnterior)
                .HasMaxLength(1)
                .HasColumnName("conteudoAnterior");
            entity.Property(e => e.UltimoReflesh)
                .HasColumnType("datetime")
                .HasColumnName("ultimoReflesh");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.IdNota).HasName("PK__Notas__AD5F462E3CF45821");

            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("dataCriacao");
            entity.Property(e => e.IdAnexo).HasColumnName("idAnexo");
            entity.Property(e => e.IdHistoNota).HasColumnName("idHistoNota");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UltimoRefresh)
                .HasColumnType("datetime")
                .HasColumnName("ultimoRefresh");

            entity.HasOne(d => d.IdAnexoNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdAnexo)
                .HasConstraintName("FK__Notas__idAnexo__656C112C");

            entity.HasOne(d => d.IdHistoNotaNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdHistoNota)
                .HasConstraintName("FK__Notas__idHistoNo__66603565");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Notas__idUsuario__6477ECF3");
        });

        modelBuilder.Entity<NotaTag>(entity =>
        {
            entity.HasKey(e => e.IdNotaTag).HasName("PK__NotaTag__97932B08DDE57827");

            entity.ToTable("NotaTag");

            entity.Property(e => e.IdNotaTag).HasColumnName("idNotaTag");
            entity.Property(e => e.IdNota).HasColumnName("idNota");
            entity.Property(e => e.IdTag).HasColumnName("idTag");

            entity.HasOne(d => d.IdNotaNavigation).WithMany(p => p.NotaTags)
                .HasForeignKey(d => d.IdNota)
                .HasConstraintName("FK__NotaTag__idNota__693CA210");

            entity.HasOne(d => d.IdTagNavigation).WithMany(p => p.NotaTags)
                .HasForeignKey(d => d.IdTag)
                .HasConstraintName("FK__NotaTag__idTag__6A30C649");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("PK__TAG__020FEDB8B1C9A31B");

            entity.ToTable("TAG");

            entity.Property(e => e.IdTag).HasColumnName("idTag");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6648953B7");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fonte)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
            entity.Property(e => e.Tema)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(100)
                .HasColumnName("urlFoto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
