using Microsoft.AspNetCore.Mvc;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels;
using AlmoxarifadoAPI.Extensions;
using AlmoxarifadoServices.ViewModels.Produto;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System.Data.Common;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.Requisicao;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;

namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/SaidaFiscal")]
    public class GestaoSaidaController : ControllerBase
    {
        private readonly IGestaoRequisicaoService _gestaoService;

        public GestaoSaidaController(IGestaoRequisicaoService gestaoService)
        {
            _gestaoService = gestaoService;
        }


   

        [HttpPost("registrar/requisicao")]
        public async Task<IActionResult> RegistrarRequisicao(CreateRequisicaoViewModel notaFiscal)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CreateRequisicaoViewModel>(ModelState.GetErrors()));

            try
            {
                var requisicaoRegistrada = await _gestaoService.RegistrarRequisicao(notaFiscal);
                return Ok(new ResultViewModel<Requisicao>(requisicaoRegistrada));
            }
            catch (DbException)
            {
                return StatusCode(500, new ResultViewModel<Requisicao>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
            catch(ArgumentException e)
            {
                return BadRequest(new ResultViewModel<Requisicao>(e.Message.Normalize()));
            }
        }

        [HttpPost("registrar/itemrequisicao/{id}")]
        public async Task<IActionResult> RegistrarItemRequisicao(int id, CreateItemRequisicaoViewModel itemReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CreateItemRequisicaoViewModel>(ModelState.GetErrors()));

            try
            {
                var itemReqRegistrado = await _gestaoService.RegistrarItemRequisicao(id, itemReq);
                return Ok(new ResultViewModel<ItensReq>(itemReqRegistrado));
            }
            catch (DbException)
            {
                return StatusCode(500, new ResultViewModel<ItensReq>("Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde."));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new ResultViewModel<ItensReq>(e.Message.Normalize()));
            }
        }


    }
}
