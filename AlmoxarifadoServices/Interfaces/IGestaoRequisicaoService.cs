using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoRequisicaoService
    {
        Task<RequisicaoComItensGetDTO> CriarItens(
            List<ItemRequisicaoPostDTO> itens,
            RequisicaoGetDTO model
        );
    }
}
