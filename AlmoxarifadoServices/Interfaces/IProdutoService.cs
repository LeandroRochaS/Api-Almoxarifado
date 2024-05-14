using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {

        Task<Produto> CreateV2(CreateProdutoViewModel entity);
    }
}
