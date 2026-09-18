using System;
using System.Collections.Generic;
using BolosDoJacquinWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.BdContextEvent;

public partial class BolosJacquinContext : DbContext
{
    public BolosJacquinContext()
    {
    }

    public BolosJacquinContext(DbContextOptions<BolosJacquinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacao { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PK__Avaliaca__78C432D839EA6241");

            entity.Property(e => e.IdAvaliacao).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Avaliacao).HasConstraintName("FK__Avaliacao__IdPro__6D0D32F4");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Avaliacao).HasConstraintName("FK__Avaliacao__IdUsu__6E01572D");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A102812EA7A");

            entity.Property(e => e.IdCategoria).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PK__Produto__2E883C239F078306");

            entity.Property(e => e.IdProduto).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produto).HasConstraintName("FK__Produto__IdCateg__693CA210");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Produto).HasConstraintName("FK__Produto__IdUsuar__68487DD7");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__TipoUsua__CA04062B102CFC82");

            entity.Property(e => e.IdTipoUsuario).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9735E297C9");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuario).HasConstraintName("FK__Usuario__IdTipoU__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
