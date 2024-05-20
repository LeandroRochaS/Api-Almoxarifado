using AlmoxarifadoServices.Implementations;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/SaidaFiscal")]
    public class GestaoSaidaController : ControllerBase
    {
        private readonly IGestaoRequisicaoService _gestaoService;
        private readonly IRequisicaoService _requisicaoService;

        public GestaoSaidaController(
            IGestaoRequisicaoService gestaoService,
            IRequisicaoService requisicaoService
        )
        {
            _gestaoService = gestaoService;
            _requisicaoService = requisicaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequisicaoComitens(
            [FromBody] RequisicaoComItensPostDTO model
        )
        {
            if (
                model == null
                || model.Requisicao == null
                || model.Itens == null
                || model.Itens.Count == 0
            )
                return BadRequest("Dados inválidos");

            try
            {
                var requisicao = await _requisicaoService.Create(model.Requisicao);

                if (requisicao == null)
                    return BadRequest(new ResultViewModel<string>("Erro ao criar nota fiscal"));

                var notaFiscalGet = await _gestaoService.CriarItens(model.Itens, requisicao);

                return Ok(notaFiscalGet);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
