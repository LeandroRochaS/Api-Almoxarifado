using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using AlmoxarifadoServices.DTO;

>>>>>>> Stashed changes

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoRequisicaoService
    {
<<<<<<< Updated upstream
        Task<Requisicao> RegistrarRequisicao(Requisicao requisicao);

        Task<ItensReq> RegistrarItemRequisicao(ItensReq itemRequisicao);
=======
        Task<RequisicaoComItensGetDTO> CriarItens(List<ItemRequisicaoPostDTO> itens, RequisicaoGetDTO model);
>>>>>>> Stashed changes
    }
}
