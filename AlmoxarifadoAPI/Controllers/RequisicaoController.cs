using System;
using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicaoController : ControllerBase
    {
        private readonly IRequisicaoService _requisicaoService;
        private readonly IGestaoRequisicaoService _gestaoRequisicaoService;

        public RequisicaoController(
            IRequisicaoService requisicaoService,
            IGestaoRequisicaoService gestaoRequisicaoService
        )
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
                return Ok(new ResultViewModel<IEnumerable<RequisicaoGetDTO>>(requisicoes));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
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
                return Ok(new ResultViewModel<RequisicaoGetDTO>(requisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostRequisicao([FromBody] RequisicaoPostDTO requisicao)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _requisicaoService.Create(requisicao);
                return CreatedAtAction(
                    nameof(GetRequisicao),
                    new { id = newRequisicao.IdReq },
                    new ResultViewModel<RequisicaoGetDTO>(newRequisicao)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequisicao(
            int id,
            [FromBody] RequisicaoPostDTO requisicao
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
                var updatedRequisicao = await _requisicaoService.Update(id, requisicao);
                if (updatedRequisicao == null)
                {
                    return NotFound(new ResultViewModel<string>("Requisição não encontrada."));
                }
                return Ok(new ResultViewModel<RequisicaoGetDTO>(updatedRequisicao));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
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
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResultViewModel<string>(ex.Message)
                );
            }
        }
    }
}
