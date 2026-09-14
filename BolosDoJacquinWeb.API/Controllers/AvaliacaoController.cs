using BolosDoJacquinWeb.API.DTO;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquinWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacao;

        public AvaliacaoController(IAvaliacao avaliacao)
        {
            _avaliacao = avaliacao;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _avaliacao.Listar();
                return Ok(tipos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var avaliacao = await _avaliacao.BuscarPorId(id);
                return Ok(avaliacao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] AvaliacaoDTO dto)
        {
            var avaliacao = new Avaliacao()
            {
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                MotivoOcultacao = dto.MotivacaoOcultacao,
                DataCriacao = dto.DataCriacao,
                DataAlteracao = dto.DataAlteracao,
                Situacao = dto.Situacao,
                IdUsuario = dto.IdUsuario,
                IdProduto = dto.IdProduto
            };

            await _avaliacao.Cadastrar(avaliacao);
            return StatusCode(201, avaliacao);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AvaliacaoDTO dto)
        {
            var avaliacao = new Avaliacao()
            {
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                MotivoOcultacao = dto.MotivacaoOcultacao,
                DataCriacao = dto.DataCriacao,
                DataAlteracao = dto.DataAlteracao,
                Situacao = dto.Situacao
            };

            await _avaliacao.Atualizar(id, avaliacao);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _avaliacao.Deletar(id);
            return NoContent();
        }
    }
}
