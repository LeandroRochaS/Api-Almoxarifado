using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemNotaFiscalController : ControllerBase
    {
        private readonly IItemNotaService _itemNotaService;
        private readonly IGestaoNotaFiscalService _gestaoNotaFiscalService;

        public ItemNotaFiscalController(IItemNotaService itemNotaService, IGestaoNotaFiscalService gestaoNotaFiscalService)
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

<<<<<<< HEAD
                return Ok(new ResultViewModel<IEnumerable<ItemNotaFiscalGetDTO>>(paginatedItens));
=======
                return Ok(new ResultViewModel<IEnumerable<ItensNotum>>(paginatedItens));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
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
                    return NotFound(new ResultViewModel<string>("Item de nota fiscal não encontrado."));
                }
<<<<<<< HEAD
                return Ok(new ResultViewModel<ItemNotaFiscalGetDTO>(item));
=======
                return Ok(new ResultViewModel<ItensNotum>(item));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // POST: api/ItemNotaFiscal
        [HttpPost("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> PostItemNotaFiscal(int id, [FromBody] ItemNotaFiscalPostDTO item)
=======
        public async Task<IActionResult> PostItemNotaFiscal(int id, [FromBody] CreateItemNotaFiscalViewModel item)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensNotum>(ModelState.GetErrors()));
            try
            {
                var newItem = await _itemNotaService.Create(id, item);
<<<<<<< HEAD
                return CreatedAtAction(nameof(GetItemNotaFiscal), new { id = newItem.IdNota }, new ResultViewModel<ItemNotaFiscalGetDTO>(newItem));
=======
                return CreatedAtAction(nameof(GetItemNotaFiscal), new { id = newItem.IdNota }, new ResultViewModel<ItensNotum>(newItem));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // PUT: api/ItemNotaFiscal/5
        [HttpPut("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> PutItemNotaFiscal(int id, [FromBody] ItemNotaFiscalPutDTO item)
=======
        public async Task<IActionResult> PutItemNotaFiscal(int id, [FromBody] ItensNotum item)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensNotum>(ModelState.GetErrors()));
            try
            {
                var updatedItem = await _itemNotaService.Update(id, item);
                if (updatedItem == null)
                {
                    return NotFound(new ResultViewModel<string>("Item de nota fiscal não encontrado."));
                }
<<<<<<< HEAD
                return Ok(new ResultViewModel<ItemNotaFiscalGetDTO>(updatedItem));
=======
                return Ok(new ResultViewModel<ItensNotum>(updatedItem));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
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
                    return NotFound(new ResultViewModel<string>("Item de nota fiscal não encontrado."));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
