using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Repositories
{
    public class UnidadeDeMedidaRepository : IUnidadeDeMedidaRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public UnidadeDeMedidaRepository(xAlmoxarifadoContext context)
        {
            _context = context;
        }
        public async Task<UnidadeMedidum> GetById(int id)
        {
            return await _context.UnidadeMedida.FirstOrDefaultAsync(x => x.IdUnMed == id);
        }
    }
}
