using BolosDoJacquinWeb.API.Models;

namespace BolosDoJacquinWeb.API.Interfaces
{
    public interface IAvaliacao
    {
        Task Cadastrar(Avaliacao avaliacao);

        Task Atualizar(Guid id, Avaliacao avaliacao);

        Task Deletar(Guid id);

        Task<List<Avaliacao>> Listar();

        Task<Avaliacao?> BuscarPorId(Guid id);
    }
}
