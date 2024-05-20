using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
=======
using AlmoxarifadoServices.Implementations;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using Microsoft.AspNetCore.Http;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemRequisicaoController : ControllerBase
    {

        private readonly IItemRequisicaoService _itemService;
        private readonly IGestaoRequisicaoService _gestaoReqService;

        public ItemRequisicaoController(IItemRequisicaoService itemService, IGestaoRequisicaoService gestaoRequisicaoService)
        {
            _itemService = itemService;
            _gestaoReqService = gestaoRequisicaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisicoes()
        {
            try
            {
                var requisicoes = await _itemService.GetAll();
<<<<<<< HEAD
                return Ok(new ResultViewModel<IEnumerable<ItemRequisicaoGetDTO>>(requisicoes));
=======
                return Ok(new ResultViewModel<IEnumerable<ItensReq>>(requisicoes));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // GET: api/Requisicao/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequisicao(int id)
        {
            try
            {
                var requisicao = await _itemService.GetById(id);
                if (requisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
<<<<<<< HEAD
                return Ok(new ResultViewModel<ItemRequisicaoGetDTO>(requisicao));
=======
                return Ok(new ResultViewModel<ItensReq>(requisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // POST: api/Requisicao
        [HttpPost("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> PostRequisicao(int id, [FromBody] ItemRequisicaoPostDTO requisicao)
=======
        public async Task<IActionResult> PostRequisicao(int id, [FromBody] CreateItemRequisicaoViewModel requisicao)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensReq>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _itemService.Create(id, requisicao);
<<<<<<< HEAD
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<ItemRequisicaoGetDTO>(newRequisicao));
=======
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<ItensReq>(newRequisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // PUT: api/Requisicao/5
        [HttpPut("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] ItemRequisicaoPutDTO requisicao)
=======
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] ItensReq requisicao)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {


                var updatedRequisicao = await _itemService.Update(id, requisicao);
                if (updatedRequisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
<<<<<<< HEAD
                return Ok(new ResultViewModel<ItemRequisicaoGetDTO>(updatedRequisicao));
=======
                return Ok(new ResultViewModel<ItensReq>(updatedRequisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // DELETE: api/Requisicao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequisicao(int id)
        {
            try
            {
                var requisicaoToDelete = await _itemService.Delete(id);
                if (requisicaoToDelete == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
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
