using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IRequisicaoService 
    {

        Task<Requisicao> Create(CreateRequisicaoViewModel requisicaoView);
        Task<Requisicao> Update(int id, Requisicao entity);
        Task<Requisicao> GetById(int id);
        Task<IEnumerable<Requisicao>> GetAll();
        Task<Requisicao> Delete(int id);
    }
}
