using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;


namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemNotaService 
    {
        Task<IEnumerable<ItensNotum>> GetAll();
        Task<ItensNotum> GetById(int id);
        Task<ItensNotum> Create(int id, CreateItemNotaFiscalViewModel entity);
        Task<ItensNotum> Update(int id, ItensNotum entity);

        Task<ItensNotum> Delete(int id);
    }
}
