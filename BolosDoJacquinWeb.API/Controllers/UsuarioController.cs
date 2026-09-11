using BolosDoJacquinWeb.API.DTO;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquinWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _usuario.Listar();
                return Ok(tipos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            var usuario = new Usuario()
            {
                IdTipoUsuario = dto.IdTipoUsuario,
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            await _usuario.Cadastrar(usuario);
            return StatusCode(201, usuario);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioDTO dto)
        {
            var usuario = new Usuario()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            await _usuario.Atualizar(id, usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id:guid}")]
    }
}
