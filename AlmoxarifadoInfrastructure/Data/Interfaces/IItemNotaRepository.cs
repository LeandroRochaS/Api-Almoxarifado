using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Interfaces
{
    public interface IItemNotaRepository 
    {
        Task<IEnumerable<ItensNotum>> GetAll();
        Task<ItensNotum> GetById(int itemNum, int idPro, int idNota, int idSec);
        Task<ItensNotum> Create(ItensNotum entity);
        Task<int> Update(ItensNotum entity);
        Task<ItensNotum> Delete(ItensNotum id);
    }
}
