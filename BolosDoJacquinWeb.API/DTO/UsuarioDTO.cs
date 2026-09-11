namespace BolosDoJacquinWeb.API.DTO
{
    public class UsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public Guid? IdTipoUsuario { get; set; }
    }
}
