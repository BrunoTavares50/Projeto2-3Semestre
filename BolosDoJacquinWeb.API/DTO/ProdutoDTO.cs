using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquinWeb.API.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        [StringLength(200, ErrorMessage = "o caminho da imagem pode conter no máximo 200 caracteres.")]
        public string ImagemUrl { get; set; } = string.Empty;

        public IFormFile? ArquivoImagem { get; set; }

        public string DescricaoCurta { get; set; } = string.Empty;

        public string DescricaoLonga { get; set; } = string.Empty;

        public string Disponibilidade { get; set; } = string.Empty;

        public bool Situacao { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdCategoria { get; set; }
    }
}
