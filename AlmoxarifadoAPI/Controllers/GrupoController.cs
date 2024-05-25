using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlmoxarifadoAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _grupoService;
        public GrupoController(IGrupoService grupoService)
        {
            _grupoService = grupoService;
        }


        [HttpGet]
        public IActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var grupos = _grupoService.ObterTodosGrupos()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return Ok(grupos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde.");
            }
        }


        [HttpGet("/Grupo/{id}")]
        public IActionResult GetPorID(int id)
        {
            try
            {
                var grupo = _grupoService.ObterGrupoPorID(id);
                if (grupo == null)
                {
                    return StatusCode(404, "Nenhum Usuario Encontrado com Esse Codigo");
                }
                return Ok(grupo);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde.");
            }

        }

        [HttpPost]
        public IActionResult CriarGrupo(GrupoPostDTO grupo)
        {
            try
            {
                 var grupoSalvo = _grupoService.CriarGrupo(grupo);
                  return Ok(grupoSalvo);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocorreu um erro ao acessar os dados. Por favor, tente novamente mais tarde.");
            }
        }
    }
}
