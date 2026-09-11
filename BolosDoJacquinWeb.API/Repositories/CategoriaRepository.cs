using BolosDoJacquinWeb.API.BdContextEvent;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        private readonly BolosJacquinContext _context;

        public CategoriaRepository(BolosJacquinContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Categoria categoria)
        {
            var categoriaBuscada = await _context.Categoria.FindAsync(id);

            if (categoriaBuscada != null)
            {
                categoriaBuscada.Nome = categoria.Nome;

                _context.Categoria.Update(categoriaBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Categoria?> BuscarPorId(Guid id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var categoriaBuscada = await _context.Categoria.FindAsync(id);

            if (categoriaBuscada != null)
            {
                _context.Categoria.Remove(categoriaBuscada);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categoria.AsNoTracking().ToListAsync();
        }
    }
}
