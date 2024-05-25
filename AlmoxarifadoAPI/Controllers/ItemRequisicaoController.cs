using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Implementations;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ItemRequisicaoController : ControllerBase
    {
        private readonly IItemRequisicaoService _itemService;
        private readonly IGestaoRequisicaoService _gestaoReqService;

        public ItemRequisicaoController(
            IItemRequisicaoService itemService,
            IGestaoRequisicaoService gestaoRequisicaoService
        )
        {
            _itemService = itemService;
            _gestaoReqService = gestaoRequisicaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisicoes(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var requisicoes = await _itemService.GetAll();

                var paginatedItens = requisicoes.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                return Ok(new ResultViewModel<IEnumerable<ItemRequisicaoGetDTO>>(paginatedItens));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // GET: api/Requisicao/5
        [HttpGet("obterReq")]
        public async Task<IActionResult> GetRequisicao([BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdRequisicao, [BindRequired] int IdSecretaria)
        {
            try
            {
                var requisicao = await _itemService.GetByIds(new KeyItemRequisicaoDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdRequisicao = IdRequisicao,
                    IdSecretaria = IdSecretaria
                });
                if (requisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return Ok(new ResultViewModel<ItemRequisicaoGetDTO>(requisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // POST: api/Requisicao
        [HttpPost("{id}")]
        public async Task<IActionResult> PostRequisicao(
            int id,
            [FromBody] ItemRequisicaoPostDTO requisicao
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensReq>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _itemService.Create(id, requisicao);
                return CreatedAtAction(
                    nameof(GetRequisicao),
                    new { id = newRequisicao.IdReq },
                    new ResultViewModel<ItemRequisicaoGetDTO>(newRequisicao)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // PUT: api/Requisicao/5
        [HttpPut]
        public async Task<IActionResult> PutRequisicao(
            [BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdRequisicao, [BindRequired] int IdSecretaria,
            [FromBody] ItemRequisicaoPutDTO requisicao
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
                var updatedRequisicao = await _itemService.Update(new KeyItemRequisicaoDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdRequisicao = IdRequisicao,
                    IdSecretaria = IdSecretaria
                }, requisicao);
                if (updatedRequisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return Ok(new ResultViewModel<ItemRequisicaoGetDTO>(updatedRequisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // DELETE: api/Requisicao/5
        [HttpDelete]
        public async Task<IActionResult> DeleteRequisicao([BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdRequisicao, [BindRequired] int IdSecretaria)
        {
            try
            {
                var requisicaoToDelete = await _itemService.Delete(new KeyItemRequisicaoDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdRequisicao = IdRequisicao,
                    IdSecretaria = IdSecretaria
                });
                if (requisicaoToDelete == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }
    }
}
