using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<Estoque> Create(Estoque entity);
        Task<Estoque> Delete(Estoque entity);
        Task<IEnumerable<Estoque>> GetAll();
        Task<Estoque> GetById(int id, int idSec);
        Task<Estoque> Update(Estoque entity);
    }
}
