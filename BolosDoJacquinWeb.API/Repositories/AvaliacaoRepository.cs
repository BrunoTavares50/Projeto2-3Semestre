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

        public async Task Atualizar(Guid id, Avaliacao avaliacao)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FindAsync(id);

            if (avaliacaoBuscada != null)
            {
                avaliacaoBuscada.Nota = avaliacao.Nota;
                avaliacaoBuscada.Comentario = avaliacao.Comentario;
                avaliacaoBuscada.MotivoOcultacao = avaliacao.MotivoOcultacao;
                avaliacaoBuscada.DataCriacao = avaliacao.DataCriacao;
                avaliacaoBuscada.DataAlteracao = avaliacao.DataAlteracao;
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
    }
}
