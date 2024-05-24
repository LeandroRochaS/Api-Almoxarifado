using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemNotaFiscalController : ControllerBase
    {
        private readonly IItemNotaService _itemNotaService;
        private readonly IGestaoNotaFiscalService _gestaoNotaFiscalService;

        public ItemNotaFiscalController(
            IItemNotaService itemNotaService,
            IGestaoNotaFiscalService gestaoNotaFiscalService
        )
        {
            _itemNotaService = itemNotaService;
            _gestaoNotaFiscalService = gestaoNotaFiscalService;
        }

        // GET: api/ItemNotaFiscal
        [HttpGet]
        public async Task<IActionResult> GetItensNotaFiscal(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var itens = await _itemNotaService.GetAll();

                var paginatedItens = itens.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                return Ok(new ResultViewModel<IEnumerable<ItemNotaFiscalGetDTO>>(paginatedItens));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // GET: api/ItemNotaFiscal/5
        [HttpGet("obterNota")]
        public async Task<IActionResult> GetItemNotaFiscal([BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdNota, [BindRequired] int IdSecretaria)
        {
            try
            {
                var item = await _itemNotaService.GetById(new KeyItemNotaDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdNota = IdNota,
                    IdSecretaria = IdSecretaria
                });
                if (item == null)
                {
                    return NotFound(
                        new ResultViewModel<string>("Item de nota fiscal não encontrado.")
                    );
                }
                return Ok(new ResultViewModel<ItemNotaFiscalGetDTO>(item));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // POST: api/ItemNotaFiscal
        [HttpPost("{id}")]
        public async Task<IActionResult> PostItemNotaFiscal(
            int id,
            [FromBody] ItemNotaFiscalPostDTO item
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensNotum>(ModelState.GetErrors()));
            try
            {
                var newItem = await _itemNotaService.Create(id, item);
                return CreatedAtAction(
                    nameof(GetItemNotaFiscal),
                    new { id = newItem.IdNota },
                    new ResultViewModel<ItemNotaFiscalGetDTO>(newItem)
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

        // PUT: api/ItemNotaFiscal/5
        [HttpPut]
        public async Task<IActionResult> PutItemNotaFiscal(
           [BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdNota, [BindRequired] int IdSecretaria,
            [FromBody] ItemNotaFiscalPutDTO item
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensNotum>(ModelState.GetErrors()));
            try
            {
                var updatedItem = await _itemNotaService.Update(new KeyItemNotaDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdNota = IdNota,
                    IdSecretaria = IdSecretaria
                }, item);
                if (updatedItem == null)
                {
                    return NotFound(
                        new ResultViewModel<string>("Item de nota fiscal não encontrado.")
                    );
                }
                return Ok(new ResultViewModel<ItemNotaFiscalGetDTO>(updatedItem));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        // DELETE: api/ItemNotaFiscal/5
        [HttpDelete]
        public async Task<IActionResult> DeleteItemNotaFiscal([BindRequired] int NumItem, [BindRequired] int IdProduto, [BindRequired] int IdNota, [BindRequired] int IdSecretaria)
        {
            try
            {
                var itemToDelete = await _itemNotaService.Delete(new KeyItemNotaDTO
                {
                    NumItem = NumItem,
                    IdProduto = IdProduto,
                    IdNota = IdNota,
                    IdSecretaria = IdSecretaria
                });
                if (itemToDelete == null)
                {
                    return NotFound(
                        new ResultViewModel<string>("Item de nota fiscal não encontrado.")
                    );
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
