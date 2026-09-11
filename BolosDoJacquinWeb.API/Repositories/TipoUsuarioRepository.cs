using BolosDoJacquinWeb.API.BdContextEvent;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        private readonly BolosJacquinContext _context;

        public TipoUsuarioRepository(BolosJacquinContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var tipoUsuarioBuscado = await _context.TipoUsuario.FindAsync(id);

            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

                _context.TipoUsuario.Update(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoUsuario?> BuscarPorId(Guid id)
        {
            return await _context.TipoUsuario.FirstOrDefaultAsync(t => t.IdTipoUsuario == id);
        }

        public async Task Cadastrar(TipoUsuario tipoUsuario)
        {
            await _context.TipoUsuario.AddAsync(tipoUsuario);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoUsuarioBuscado = await _context.TipoUsuario.FindAsync(id);

            if (tipoUsuarioBuscado != null)
            {
                _context.TipoUsuario.Remove(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TipoUsuario>> Listar()
        {
            return await _context.TipoUsuario.AsNoTracking().ToListAsync();
        }
    }
}
