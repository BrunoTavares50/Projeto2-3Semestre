using BolosDoJacquinWeb.API.BdContextEvent;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Repositories
{
    public class ProdutoRepository : IProduto
    {
        private readonly BolosJacquinContext _context;

        public ProdutoRepository(BolosJacquinContext context)
        {
            _context = context;
        }

        public async Task AlterarDisponibilidade(Guid id, string disponibilidade)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                produtoBuscado.Disponibilidade = disponibilidade;

                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AlterarSituacao(Guid id, bool situacao)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                produtoBuscado.Situacao = situacao;

                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Atualizar(Guid id, Produto produto)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                produtoBuscado.Nome = produto.Nome;
                produtoBuscado.Preco = produto.Preco;
                produtoBuscado.ImagemUrl = produto.ImagemUrl;
                produtoBuscado.DescricaoCurta = produto.DescricaoCurta;
                produtoBuscado.DescricaoLonga = produto.DescricaoLonga;
                produtoBuscado.Disponibilidade = produto.Disponibilidade;
                produtoBuscado.Situacao = produto.Situacao;

                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Produto?> BuscarPorId(Guid id)
        {
            return await _context.Produto.FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task Cadastrar(Produto produto)
        {
            await _context.Produto.AddAsync(produto);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                _context.Produto.Remove(produtoBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Produto>> Filtrar(Guid? idCategoria, decimal? precoMin, decimal? precoMax, string? termoBusca)
        {
            var query = _context.Produto.AsNoTracking();

            if (idCategoria.HasValue)
            {
                query = query.Where(p => p.IdCategoria == idCategoria);
            }

            if (precoMin.HasValue)
            {
                query = query.Where(p => p.Preco >= precoMin);
            }

            if (precoMax.HasValue)
            {
                query = query.Where(p => p.Preco <= precoMax);
            }

            if (!string.IsNullOrEmpty(termoBusca))
            {
                query = query.Where(p =>
                    p.Nome.ToLower().Contains(termoBusca.ToLower()));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Produto>> Listar()
        {
            return await _context.Produto.AsNoTracking().ToListAsync();
        }
    }
}
