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
    public class SetorRepository : ISetorRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public SetorRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }
        public async Task<Setor> GetById(int id)
        {

            return await _context.Setors.FirstOrDefaultAsync(x => x.IdSet == id);
        }
    }
}
