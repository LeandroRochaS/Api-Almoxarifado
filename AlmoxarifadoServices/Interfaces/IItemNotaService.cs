using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

=======
using AlmoxarifadoServices.DTO;
>>>>>>> Stashed changes

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemNotaService 
    {
<<<<<<< HEAD
<<<<<<< Updated upstream

=======
        Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll();
        Task<ItemNotaFiscalGetDTO> GetById(int id);
        Task<ItemNotaFiscalGetDTO> Create(int id, ItemNotaFiscalPostDTO entity);
        Task<ItemNotaFiscalGetDTO> Update(int id, ItemNotaFiscalPutDTO entity);

        Task<ItemNotaFiscalGetDTO> Delete(int id);
>>>>>>> Stashed changes
=======
        Task<IEnumerable<ItensNotum>> GetAll();
        Task<ItensNotum> GetById(int id);
        Task<ItensNotum> Create(int id, CreateItemNotaFiscalViewModel entity);
        Task<ItensNotum> Update(int id, ItensNotum entity);

        Task<ItensNotum> Delete(int id);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
