using BolosDoJacquinWeb.API.Models;

namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface ITipoUsuario
    {
        Task Cadastrar(TipoUsuario tipoUsuario);

        Task Atualizar(Guid id, TipoUsuario tipoUsuario);

        Task Deletar(Guid id);

        Task<List<TipoUsuario>> Listar();

        Task<TipoUsuario?> BuscarPorId(Guid id);
    }
}
