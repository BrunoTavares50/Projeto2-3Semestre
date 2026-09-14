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

        Task<List<Produto>> Filtrar(Guid? idCategoria, decimal? precoMin, decimal? precoMax, string? termoBusca);

        Task AlterarDisponibilidade(Guid id, string disponibilidade);

        Task AlterarSituacao(Guid id, bool situacao);
    }
}
