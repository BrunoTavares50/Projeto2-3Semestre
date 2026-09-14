using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Models;

public partial class Avaliacao
{
    [Key]
    public Guid IdAvaliacao { get; set; }

    public Guid? IdProduto { get; set; }

    public Guid? IdUsuario { get; set; }

    public float Nota { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Comentario { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string MotivoOcultacao { get; set; } = null!;

    public bool Situacao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataCriacao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataAlteracao { get; set; }

    [ForeignKey("IdProduto")]
    [InverseProperty("Avaliacao")]
    public virtual Produto? IdProdutoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Avaliacao")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
