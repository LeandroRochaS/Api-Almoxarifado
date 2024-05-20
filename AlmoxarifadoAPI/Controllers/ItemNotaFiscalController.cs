using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemNotaFiscal(int id)
        {
            try
            {
                var item = await _itemNotaService.GetById(id);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemNotaFiscal(
            int id,
            [FromBody] ItemNotaFiscalPutDTO item
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensNotum>(ModelState.GetErrors()));
            try
            {
                var updatedItem = await _itemNotaService.Update(id, item);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemNotaFiscal(int id)
        {
            try
            {
                var itemToDelete = await _itemNotaService.Delete(id);
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
