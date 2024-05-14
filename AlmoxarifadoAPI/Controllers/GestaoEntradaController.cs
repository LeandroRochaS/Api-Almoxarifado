using Microsoft.AspNetCore.Mvc;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoServices.ViewModels.Produto;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System.Data.Common;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;

namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/EntradaFiscal")]
    public class GestaoEntradaController : ControllerBase
    {
        private readonly IGestaoNotaFiscalService _gestaoService;

        public GestaoEntradaController(IGestaoNotaFiscalService gestaoService)
        {
            _gestaoService = gestaoService;
        }


   

        [HttpPost("registrar/notafiscal")]
        public async Task<IActionResult> RegistrarNotaFiscal(CreateNotaFiscalViewModel notaFiscal)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CreateNotaFiscalViewModel>(ModelState.GetErrors()));

            try
            {
                var notaFiscalRegistrada = await _gestaoService.RegistroDeNotaFiscal(notaFiscal);
                return Ok(new ResultViewModel<NotaFiscal>(notaFiscalRegistrada));
            }
            catch (DbException)
            {
                return StatusCode(500, new ResultViewModel<NotaFiscal>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
            catch(ArgumentException e)
            {
                return BadRequest(new ResultViewModel<NotaFiscal>(e.Message.Normalize()));
            }
        }

        [HttpPost("registrar/itemfiscal/{id}")]
        public async Task<IActionResult> RegistrarNotaFiscal(int id, CreateItemNotaFiscaViewModel itemFiscal)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CreateNotaFiscalViewModel>(ModelState.GetErrors()));

            try
            {
                var itemFiscalRegistrado = await _gestaoService.RegistrarItemDeNotaFiscal(id, itemFiscal);
                return Ok(new ResultViewModel<ItensNotum>(itemFiscalRegistrado));
            }
            catch (DbException)
            {
                return StatusCode(500, new ResultViewModel<ItensNotum>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ResultViewModel<ItensNotum>(e.Message.Normalize()));
            }
        }


    }
}
