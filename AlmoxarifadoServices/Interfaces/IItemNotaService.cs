using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream

=======
using AlmoxarifadoServices.DTO;
>>>>>>> Stashed changes

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemNotaService : IServiceBase<ItensNotum>
    {
<<<<<<< Updated upstream

=======
        Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll();
        Task<ItemNotaFiscalGetDTO> GetById(int id);
        Task<ItemNotaFiscalGetDTO> Create(int id, ItemNotaFiscalPostDTO entity);
        Task<ItemNotaFiscalGetDTO> Update(int id, ItemNotaFiscalPutDTO entity);

        Task<ItemNotaFiscalGetDTO> Delete(int id);
>>>>>>> Stashed changes
    }
}
