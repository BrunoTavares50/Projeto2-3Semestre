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
                DataCriacao = DateTime.UtcNow,
                DataAlteracao = DateTime.UtcNow,
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
            try
            {
                var avaliacao = new Avaliacao()
                {
                    Nota = dto.Nota,
                    Comentario = dto.Comentario,
                    MotivoOcultacao = dto.MotivacaoOcultacao,
                    DataAlteracao = DateTime.UtcNow,
                    Situacao = dto.Situacao
                };

                await _avaliacao.Atualizar(id, avaliacao);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _avaliacao.Deletar(id);
            return NoContent();
        }

        [HttpGet("ListarPorProduto/{id:guid}")]
        public async Task<IActionResult> ListarPorProduto(Guid id)
        {
            try
            {
                var avaliacoes = await _avaliacao.ListarPorProduto(id);

                foreach (var avaliacao in avaliacoes)
                {
                    if (!avaliacao.Situacao)
                    {
                        avaliacao.Comentario = null!;
                    }
                }

                return Ok(avaliacoes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarPorUsuario/{id:guid}")]
        public async Task<IActionResult> ListarPorUsuario(Guid id)
        {
            try
            {
                var avaliacoes = await _avaliacao.ListarPorUsuario(id);
                return Ok(avaliacoes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("AlterarSituacao/{id}")]
        public async Task<IActionResult> AlterarSituacao(Guid id, [FromBody] AlterarSituacaoAvaliacaoDTO dto)
        {
            var avaliacao = new Avaliacao()
            {
                MotivoOcultacao = dto.MotivoOcultacao,
                Situacao = dto.Situacao
            };

            await _avaliacao.AlterarSituacao(id, avaliacao.Situacao, avaliacao.MotivoOcultacao);
            return Ok();
        }
    }
}
