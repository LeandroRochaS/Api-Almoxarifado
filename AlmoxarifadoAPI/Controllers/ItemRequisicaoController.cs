using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Implementations;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using Microsoft.AspNetCore.Http;
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
                return Ok(new ResultViewModel<IEnumerable<ItensReq>>(requisicoes));
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
                return Ok(new ResultViewModel<ItensReq>(requisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // POST: api/Requisicao
        [HttpPost("{id}")]
        public async Task<IActionResult> PostRequisicao(int id, [FromBody] CreateItemRequisicaoViewModel requisicao)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<ItensReq>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _itemService.Create(id, requisicao);
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<ItensReq>(newRequisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // PUT: api/Requisicao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] ItensReq requisicao)
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
                return Ok(new ResultViewModel<ItensReq>(updatedRequisicao));
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
