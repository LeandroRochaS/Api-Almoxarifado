using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemRequisicaoService
    {
        Task<ItemRequisicaoGetDTO> Create(int id, ItemRequisicaoPostDTO itemRequisicaoView);
        Task<ItemRequisicaoGetDTO> Update(int id, ItemRequisicaoPutDTO itemRequisicao);
        Task<ItemRequisicaoGetDTO> GetById(int id);
        Task<ItemRequisicaoGetDTO> Delete(int id);
        Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll();
    }
}
