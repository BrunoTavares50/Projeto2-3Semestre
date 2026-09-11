namespace BolosDoJacquinWeb.API.DTO
{
    public class AvaliacaoDTO
    {
        public float Nota { get; set; }

        public string Comentario { get; set; } = string.Empty;

        public string MotivacaoOcultacao { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime DataAlteracao { get; set; } = DateTime.Now;

        public bool Situacao { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdProduto { get; set; }
    }
}
