using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Models;

[Index("Email", Name = "UQ__Usuario__A9D10534F3C8AAF3", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    public Guid? IdTipoUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;

    public bool Situacao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataCadastro { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();

    [ForeignKey("IdTipoUsuario")]
    [InverseProperty("Usuario")]
    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
