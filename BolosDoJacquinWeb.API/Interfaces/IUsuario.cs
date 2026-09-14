using BolosDoJacquinWeb.API.Models;

namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface IUsuario
    {
        Task Cadastrar(Usuario usuario);

        Task Atualizar(Guid id, Usuario usuario);

        Task Deletar(Guid id);

        Task<List<Usuario>> Listar();

        Task<Usuario?> BuscarPorId(Guid id);

        Task<Usuario?> BuscarPorEmailESenha(string email, string senha);

        Task AtualizarSituacao(Guid id, string situacao);
    }
}
