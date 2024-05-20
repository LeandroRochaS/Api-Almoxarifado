using Microsoft.AspNetCore.Mvc;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.DTO;


namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/EntradaFiscal")]
    public class GestaoEntradaController : ControllerBase
    {
<<<<<<< Updated upstream
        private readonly IGestaoNotaFiscalService _gestaoService;

        public GestaoEntradaController(IGestaoNotaFiscalService gestaoService)
        {
            _gestaoService = gestaoService;
        }


   

        [HttpPost("registrar/notafiscal")]
        public async Task<IActionResult> RegistrarNotaFiscal(CreateNotaFiscalViewModel notaFiscal)
=======
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IGestaoNotaFiscalService _gestaoNotaFiscalService;

        public GestaoEntradaController(INotaFiscalService notaFiscalService, IGestaoNotaFiscalService gestaoNotaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
            _gestaoNotaFiscalService = gestaoNotaFiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotaFiscalComItens([FromBody] NotaFiscalComItensPostlDTO model)
>>>>>>> Stashed changes
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
