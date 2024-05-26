using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IRequisicaoService
    {
        Task<RequisicaoGetDTO> Create(RequisicaoPostDTO requisicaoView);
        Task<RequisicaoGetDTO> Delete(int id);
        Task<IEnumerable<RequisicaoGetDTO>> GetAll();
        Task<RequisicaoGetDTO> GetById(int id);
        Task<RequisicaoGetDTO> Update(int id, RequisicaoPutDTO requisicaoView);
        Task<RequisicaoGetDTO> AtualizarTotalReq(int idReq);

        Task<RequisicaoGetDTO> AdicionarItem(int req);

        Task<RequisicaoGetDTO> RemoverItem(int req);
    }
}
