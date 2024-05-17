using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemRequisicaoService 
    {
        Task<ItensReq> Create(int id, CreateItemRequisicaoViewModel itemRequisicaoView);
        Task<ItensReq> Update(int id, ItensReq itemRequisicao);
        Task<ItensReq> GetById(int id);
        Task<ItensReq> Delete(int id);
        Task<IEnumerable<ItensReq>> GetAll();
    }
}
