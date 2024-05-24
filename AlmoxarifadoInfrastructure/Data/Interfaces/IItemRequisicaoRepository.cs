using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Interfaces
{
    public interface IItemRequisicaoRepository 
    {
        Task<IEnumerable<ItensReq>> GetAll();
        Task<ItensReq> GetById(int itemNum, int idPro, int idReq, int idSec);
        Task<ItensReq> Create(ItensReq entity);
        Task<int> Update(ItensReq entity);
        Task<ItensReq> Delete(ItensReq id);
    }
}
