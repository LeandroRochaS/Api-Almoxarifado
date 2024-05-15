using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IEstoqueService : IServiceBase<Estoque>
    {
        Task<Estoque> AdicionarEstoque(int id, decimal quantidade);

        Task<Estoque> RemoverEstoque(int id, decimal quantidade);
    }
}
