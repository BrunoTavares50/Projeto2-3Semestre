using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquinWeb.API.DTO
{
    public class AvaliacaoDTO
    {
        [Required(ErrorMessage = "A nota é obrigatória.")]
        [Range(0, 5, ErrorMessage = "A nota deve estar entre 0 e 5.")]
        public decimal Nota { get; set; }

        [Required(ErrorMessage = "O comentário é obrigatório.")]
        [MaxLength(500)]
        public string Comentario { get; set; } = string.Empty;

        public string MotivacaoOcultacao { get; set; } = string.Empty;

        public bool Situacao { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdProduto { get; set; }
    }
}
