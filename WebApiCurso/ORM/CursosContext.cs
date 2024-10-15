using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiCurso.ORM;

public partial class CursosContext : DbContext
{
    public CursosContext()
    {
    }

    public CursosContext(DbContextOptions<CursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVenda> TbVendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_4\\SQLEXPRESS;Database=Cursos;User Id=admin;Password=admin;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocDocumentacao).HasColumnName("docDocumentacao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cep)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkCliente).HasColumnName("FK_CLIENTE");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Pontodereferencia)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TbEndereco)
                .HasForeignKey<TbEndereco>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECO_TB_CLIENTE1");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.NotaFiscal).HasColumnName("notaFISCAL");
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("preco");
            entity.Property(e => e.Quanto).HasColumnName("quanto");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbVenda>(entity =>
        {
            entity.ToTable("TB_VENDA");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.FkCliente).HasColumnName("FK_CLIENTE");
            entity.Property(e => e.FkProduto).HasColumnName("FK_PRODUTO");
            entity.Property(e => e.NotaFiscal).HasColumnName("notaFISCAL");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TbVenda)
                .HasForeignKey<TbVenda>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_CLIENTE1");

            entity.HasOne(d => d.Id1).WithOne(p => p.TbVenda)
                .HasForeignKey<TbVenda>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_PRODUTO1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
