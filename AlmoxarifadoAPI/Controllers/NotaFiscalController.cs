﻿using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IGestaoNotaFiscalService _gestaoNotaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService, IGestaoNotaFiscalService gestaoNotaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
            _gestaoNotaFiscalService = gestaoNotaFiscalService;
        }

        // GET: api/NotaFiscal
        [HttpGet]
        public async Task<IActionResult> GetNotasFiscais()
        {
            try
            {
                var notasFiscais = await _notaFiscalService.GetAll();
                return Ok(new ResultViewModel<IEnumerable<NotaFiscal>>(notasFiscais));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // GET: api/NotaFiscal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotaFiscal(int id)
        {
            try
            {
                var notaFiscal = await _notaFiscalService.GetById(id);
                if (notaFiscal == null)
                {
                    return NotFound(new ResultViewModel<string>("Nota fiscal não encontrada."));
                }
                return Ok(new ResultViewModel<NotaFiscal>(notaFiscal));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // POST: api/NotaFiscal
        [HttpPost]
        public async Task<IActionResult> PostNotaFiscal([FromBody] CreateNotaFiscalViewModel notaFiscal)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<NotaFiscal>(ModelState.GetErrors()));
            try
            {
                var newNotaFiscal = await _notaFiscalService.Create(notaFiscal);
                return CreatedAtAction(nameof(GetNotaFiscal), new { id = newNotaFiscal.IdNota }, new ResultViewModel<NotaFiscal>(newNotaFiscal));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // PUT: api/NotaFiscal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaFiscal(int id, [FromBody] NotaFiscal notaFiscal)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<NotaFiscal>(ModelState.GetErrors()));
            try
            {
                var updatedNotaFiscal = await _notaFiscalService.Update(id, notaFiscal);
                if (updatedNotaFiscal == null)
                {
                    return NotFound(new ResultViewModel<string>("Nota fiscal não encontrada."));
                }
                return Ok(new ResultViewModel<NotaFiscal>(updatedNotaFiscal));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultViewModel<string>(ex.Message));
            }
        }

        // DELETE: api/NotaFiscal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaFiscal(int id)
        {
            try
            {
                var notaFiscalToDelete = await _notaFiscalService.Delete(id);
                if (notaFiscalToDelete == null)
                {
                    return NotFound(new ResultViewModel<string>("Nota fiscal não encontrada."));
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
