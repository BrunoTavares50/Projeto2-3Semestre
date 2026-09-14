using BolosDoJacquinWeb.API.DTO;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using BolosDoJacquinWeb.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produto;

        public ProdutoController(IProduto produto)
        {
            _produto = produto;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _produto.Listar();
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
                var produto = await _produto.BuscarPorId(id);
                return Ok(produto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ProdutoDTO dto)
        {
            var produto = new Produto()
            {            
                Nome = dto.Nome,
                Preco = dto.Preco,
                ImagemUrl = dto.ImagemUrl,
                DescricaoCurta = dto.DescricaoCurta,
                DescricaoLonga = dto.DescricaoLonga,
                Disponibilidade = dto.Disponibilidade,
                Situacao = dto.Situacao,
                IdUsuario = dto.IdUsuario,
                IdCategoria = dto.IdCategoria,
            };

            await _produto.Cadastrar(produto);
            return StatusCode(201, produto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProdutoDTO dto)
        {
            var produto = new Produto()
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                ImagemUrl = dto.ImagemUrl,
                DescricaoCurta = dto.DescricaoCurta,
                DescricaoLonga = dto.DescricaoLonga,
                Disponibilidade = dto.Disponibilidade,
                Situacao = dto.Situacao
            };

            await _produto.Atualizar(id, produto);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _produto.Deletar(id);
            return NoContent();
        }

        [HttpGet("Filtro")]
        public async Task<IActionResult> Filtrar(
            Guid? idCategoria, 
            decimal? precoMin, 
            decimal? precoMax, 
            string? termoBusca)
        {
            var produtos = await _produto.Filtrar(
                idCategoria,
                precoMin,
                precoMax,
                termoBusca);

            return Ok(produtos);
        }
    }
}

