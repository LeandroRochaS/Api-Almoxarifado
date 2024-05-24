using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemRequisicaoService
    {
        Task<ItemRequisicaoGetDTO> Create(int id, ItemRequisicaoPostDTO itemRequisicaoView);
        Task<ItemRequisicaoGetDTO> Update(KeyItemRequisicaoDTO keys, ItemRequisicaoPutDTO itemRequisicao);
        Task<ItemRequisicaoGetDTO> GetByIds(KeyItemRequisicaoDTO keys);
        Task<ItemRequisicaoGetDTO> Delete(KeyItemRequisicaoDTO keys);
        Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll();
    }
}
