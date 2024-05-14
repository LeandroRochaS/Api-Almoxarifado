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
    public class ClasseRepository : IClasseRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public ClasseRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }
        public async Task<Classe> GetById(int id)
        {
            return await _context.Classes.FirstOrDefaultAsync(x => x.IdClas == id);
        }
    }
}
