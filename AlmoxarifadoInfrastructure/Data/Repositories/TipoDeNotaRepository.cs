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
    public class TipoDeNotaRepository : ITipoDeNotaRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public TipoDeNotaRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }

        public async Task<TipoNotum> GetById(int id)
        {
            return await _context.TipoNota.FirstOrDefaultAsync(x => x.IdTipoNota == id);
        }
    }
}
