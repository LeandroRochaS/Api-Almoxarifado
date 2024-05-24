using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemNotaService
    {
        Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll();
        Task<ItemNotaFiscalGetDTO> GetById(KeyItemNotaDTO id);
        Task<ItemNotaFiscalGetDTO> Create(int id, ItemNotaFiscalPostDTO entity);
        Task<ItemNotaFiscalGetDTO> Update(KeyItemNotaDTO keys, ItemNotaFiscalPutDTO entity);
        Task<ItemNotaFiscalGetDTO> Delete(KeyItemNotaDTO id);
    }
}
