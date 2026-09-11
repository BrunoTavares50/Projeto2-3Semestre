using BolosDoJacquinWeb.API.Models;

namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface IProduto
    {
        Task Cadastrar(Produto produto);

        Task Atualizar(Guid id, Produto produto);

        Task Deletar(Guid id);

        Task<List<Produto>> Listar();

        Task<Produto?> BuscarPorId(Guid id);
    }
}
