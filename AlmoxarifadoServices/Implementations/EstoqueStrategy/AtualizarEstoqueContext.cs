using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations.EstoqueStrategy
{

    public class AtualizarEstoqueContext
    {
        private readonly IAtualizarEstoqueStrategy _strategy;

        public AtualizarEstoqueContext(IAtualizarEstoqueStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<Estoque> AtualizarEstoque(Estoque estoque, decimal quantidade)
        {
            return await _strategy.AtualizarEstoque(estoque, quantidade);
        }
    }
}
