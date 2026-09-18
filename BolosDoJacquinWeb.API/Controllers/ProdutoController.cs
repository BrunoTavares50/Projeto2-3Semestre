using BolosDoJacquinWeb.API.DTO;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using BolosDoJacquinWeb.API.Repositories;
using BolosDoJacquinWeb.API.Services;
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
        private readonly ICloudinaryService _cloudinaryService;

        public ProdutoController(IProduto produto, ICloudinaryService cloudinaryService)
        {
            _produto = produto;
            _cloudinaryService = cloudinaryService;
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Cadastrar([FromForm] ProdutoDTO dto)
        {
            try
            {
                string? imagemUrl = null;

                if (dto.ArquivoImagem is not null)
                    imagemUrl = await _cloudinaryService.UploadImagem(dto.ArquivoImagem);

                var produto = new Produto()
                {
                    Nome = dto.Nome,
                    Preco = dto.Preco,
                    ImagemUrl = imagemUrl,
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpPatch("Disponibilidade/{id}")]
        public async Task<IActionResult> AlterarDisponibilidade(Guid id, [FromBody] AlterarDisponibilidadeDTO dto)
        {
            await _produto.AlterarDisponibilidade(id, dto.Disponibilidade);
            return NoContent();
        }

        [HttpPatch("Situacao/{id}")]
        public async Task<IActionResult> AlterarSituacao(Guid id, [FromBody] AlterarSituacaoDTO dto)
        {
            await _produto.AlterarSituacao(id, dto.Situacao);
            return NoContent();
        }
    }
}

