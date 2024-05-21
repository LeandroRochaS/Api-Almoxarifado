using Microsoft.AspNetCore.Mvc;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.DTO;


namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/EntradaFiscal")]
    public class GestaoEntradaController : ControllerBase
    {
        private readonly IGestaoNotaFiscalService _gestaoService;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IItemNotaService _itemNotaService;
        private readonly IGestaoNotaFiscalService _gestaoNotaFiscalService;

        public GestaoEntradaController(INotaFiscalService notaFiscalService, IItemNotaService itemNotaService, IGestaoNotaFiscalService gestaoNotaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
            _itemNotaService = itemNotaService;
            _gestaoNotaFiscalService = gestaoNotaFiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotaFiscalComItens([FromBody] NotaFiscalComItensPostlDTO model)
        {
            if (model == null || model.NotaFiscal == null || model.Itens == null || model.Itens.Count == 0)
                return BadRequest("Dados inválidos");

            try
            {
                var notaFiscal = await _notaFiscalService.Create(model.NotaFiscal);

                if (notaFiscal == null)
                    return BadRequest("Erro ao criar nota fiscal");

                var notaFiscalGet = await _gestaoNotaFiscalService.CriarItens(model.Itens, notaFiscal);

                return Ok(notaFiscalGet);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
