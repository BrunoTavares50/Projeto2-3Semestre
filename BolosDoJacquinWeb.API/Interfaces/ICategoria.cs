using BolosDoJacquinWeb.API.Models;

namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface ICategoria
    {
        Task Cadastrar(Categoria categoria);

        Task Atualizar(Guid id, Categoria categoria);

        Task Deletar(Guid id);

        Task<List<Categoria>> Listar();

        Task<Categoria?> BuscarPorId(Guid id);
    }
}
