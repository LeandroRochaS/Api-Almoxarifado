using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.Requisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
                return Ok(new ResultViewModel<IEnumerable<RequisicaoGetDTO>>(requisicoes));
=======
                return Ok(new ResultViewModel<IEnumerable<Requisicao>>(requisicoes));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
                return Ok(new ResultViewModel<RequisicaoGetDTO>(requisicao));
=======
                return Ok(new ResultViewModel<Requisicao>(requisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpPost]
<<<<<<< HEAD
        public async Task<IActionResult> PostRequisicao([FromBody] RequisicaoPostDTO requisicao)
=======
        public async Task<IActionResult> PostRequisicao([FromBody] CreateRequisicaoViewModel requisicao)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Requisicao>(ModelState.GetErrors()));
            try
            {
                var newRequisicao = await _requisicaoService.Create(requisicao);
<<<<<<< HEAD
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<RequisicaoGetDTO>(newRequisicao));
=======
                return CreatedAtAction(nameof(GetRequisicao), new { id = newRequisicao.IdReq }, new ResultViewModel<Requisicao>(newRequisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        [HttpPut("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] RequisicaoPostDTO requisicao)
=======
        public async Task<IActionResult> PutRequisicao(int id, [FromBody] Requisicao requisicao)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
                return Ok(new ResultViewModel<RequisicaoGetDTO>(updatedRequisicao));
=======
                return Ok(new ResultViewModel<Requisicao>(updatedRequisicao));
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
