using Microsoft.AspNetCore.Mvc;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoServices.ViewModels.Produto;

namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("produtos")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var produtos = await _produtoService.GetAll();
                var pagedProdutos = produtos.Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
                return Ok(new ResultViewModel<List<Produto>>(pagedProdutos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
        }

        [HttpGet("produtos/{id}")]
        public async Task<IActionResult> GetPorID(int id)
        {
            try
            {
                var produto = await _produtoService.GetById(id);
                if (produto == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Nenhum produto encontrado com este ID."));
                }
                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> CriarProduto(CreateProdutoViewModel produto)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));

            try
            {
                var produtoSalvo = await _produtoService.CreateV2(produto);
                return Created($"/v1/produtos/{produtoSalvo.IdPro}", new ResultViewModel<Produto>(produtoSalvo));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
        }

        [HttpPut("produtos/{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, Produto produto)
        {
            try
            {
                var produtoAtualizado = await _produtoService.Update(id, produto);
                if (produtoAtualizado == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Nenhum produto encontrado com este ID."));
                }
                return Ok(new ResultViewModel<Produto>(produtoAtualizado));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
        }

        [HttpDelete("produtos/{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            try
            {
                await _produtoService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
        }
    }
}
