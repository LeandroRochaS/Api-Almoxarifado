using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.Requisicao;
using Microsoft.AspNetCore.Mvc;
using System;


namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicaoController : ControllerBase
    {
        private readonly IRequisicaoService _requisicaoService;
        private readonly IGestaoRequisicaoService _gestaoRequisicaoService;

        public RequisicaoController(IRequisicaoService requisicaoService, IGestaoRequisicaoService gestaoRequisicaoService)
        {
            _requisicaoService = requisicaoService;
            _gestaoRequisicaoService = gestaoRequisicaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisicoes()
        {
            try
            {
                var requisicoes = await _requisicaoService.GetAll();
                return Ok(new ResultViewModel<IEnumerable<Requisicao>>(requisicoes));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequisicao(int id)
        {
            try
            {
                var requisicao = await _requisicaoService.GetById(id);
                if (requisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return Ok(new ResultViewModel<Requisicao>(requisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostRequisicao([FromBody] CreateRequisicaoViewModel requisicao)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _gestaoRequisicaoService.RegistrarRequisicao(requisicao);
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<Requisicao>(newRequisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] Requisicao requisicao)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
               

                var updatedRequisicao = await _requisicaoService.Update(id, requisicao);
                if (updatedRequisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return Ok(new ResultViewModel<Requisicao>(updatedRequisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequisicao(int id)
        {
            try
            {
                var requisicaoToDelete = await _requisicaoService.Delete(id);
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
