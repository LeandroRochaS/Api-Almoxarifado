using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemNotaService
    {
        Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll();
        Task<ItemNotaFiscalGetDTO> GetById(int id);
        Task<ItemNotaFiscalGetDTO> Create(int id, ItemNotaFiscalPostDTO entity);
        Task<ItemNotaFiscalGetDTO> Update(int id, ItemNotaFiscalPutDTO entity);

        Task<ItemNotaFiscalGetDTO> Delete(int id);
    }
}
