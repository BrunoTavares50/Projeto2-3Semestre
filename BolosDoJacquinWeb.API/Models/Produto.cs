using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Models;

public partial class Produto
{
    [Key]
    public Guid IdProduto { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdCategoria { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    [Unicode(false)]
    public string ImagemUrl { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DescricaoCurta { get; set; } = null!;

    [Column(TypeName = "text")]
    public string DescricaoLonga { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Disponibilidade { get; set; } = null!;

    public bool Situacao { get; set; }

    [InverseProperty("IdProdutoNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Produto")]
    public virtual Categoria? IdCategoriaNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Produto")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
