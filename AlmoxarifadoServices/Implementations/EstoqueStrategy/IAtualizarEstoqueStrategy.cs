using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public interface IAtualizarEstoqueStrategy
    {
        Task<Estoque> AtualizarEstoque(Estoque estoque, decimal quantidade);
    }

}
