using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
<<<<<<< Updated upstream
        Task<Requisicao> RegistrarRequisicao(Requisicao requisicao);

        Task<ItensReq> RegistrarItemRequisicao(ItensReq itemRequisicao);
=======
        Task<RequisicaoComItensGetDTO> CriarItens(List<ItemRequisicaoPostDTO> itens, RequisicaoGetDTO model);
>>>>>>> Stashed changes
=======
        Task<GetRequisicaoComItensViewModel> CriarItens(List<CreateItemRequisicaoViewModel> itens, Requisicao model);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
