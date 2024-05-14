using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGrupoService
    {
        List<GrupoGetDTO> ObterTodosGrupos();
         Grupo ObterGrupoPorID(int id);
         GrupoGetDTO CriarGrupo(GrupoPostDTO grupo);
    }
}
