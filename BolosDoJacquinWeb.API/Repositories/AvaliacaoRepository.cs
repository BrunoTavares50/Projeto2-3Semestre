using BolosDoJacquinWeb.API.BdContextEvent;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Repositories
{
    public class AvaliacaoRepository : IAvaliacao
    {
        private readonly BolosJacquinContext _context;

        public AvaliacaoRepository(BolosJacquinContext context)
        {
            _context = context;
        }

        public async Task AlterarSituacao(Guid id, bool situacao, string disponibilidade)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FirstOrDefaultAsync(a => a.IdAvaliacao == id);

            if (avaliacaoBuscada != null)
            {
                avaliacaoBuscada.Situacao = situacao;
                avaliacaoBuscada.MotivoOcultacao = disponibilidade;
                avaliacaoBuscada.DataAlteracao = DateTime.UtcNow;

                _context.Avaliacao.Update(avaliacaoBuscada);
                await _context.SaveChangesAsync();
            }

            if (avaliacaoBuscada == null)
                throw new InvalidOperationException("Avaliação não encontrada");
        }

        public async Task Atualizar(Guid id, Avaliacao avaliacao)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FindAsync(id);

            if (avaliacaoBuscada != null)
            {
                avaliacaoBuscada.Nota = avaliacao.Nota;
                avaliacaoBuscada.Comentario = avaliacao.Comentario;
                avaliacaoBuscada.MotivoOcultacao = avaliacao.MotivoOcultacao;
                avaliacaoBuscada.DataAlteracao = DateTime.UtcNow;
                avaliacaoBuscada.Situacao = avaliacao.Situacao;

                _context.Avaliacao.Update(avaliacaoBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Avaliacao?> BuscarPorId(Guid id)
        {
            return await _context.Avaliacao.FirstOrDefaultAsync(a => a.IdAvaliacao == id);
        }

        public async Task Cadastrar(Avaliacao avaliacao)
        {
            await _context.Avaliacao.AddAsync(avaliacao);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FindAsync(id);

            if (avaliacaoBuscada != null)
            {
                _context.Avaliacao.Remove(avaliacaoBuscada);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Avaliacao>> Listar()
        {
            return await _context.Avaliacao.AsNoTracking().ToListAsync();
        }

        public async Task<List<Avaliacao>> ListarPorProduto(Guid idProduto)
        {
            return await _context.Avaliacao

                .Where(a => a.IdProduto == idProduto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Avaliacao>> ListarPorUsuario(Guid idUsuario)
        {
            return await _context.Avaliacao
                .Where(a => a.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}