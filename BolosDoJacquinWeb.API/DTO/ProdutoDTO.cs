namespace BolosDoJacquinWeb.API.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        public string ImagemUrl { get; set; } = string.Empty;

        public string DescricaoCurta { get; set; } = string.Empty;

        public string DescricaoLonga { get; set; } = string.Empty;

        public string Disponibilidade { get; set; } = string.Empty;

        public bool Situacao { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdCategoria { get; set; }
    }
}
