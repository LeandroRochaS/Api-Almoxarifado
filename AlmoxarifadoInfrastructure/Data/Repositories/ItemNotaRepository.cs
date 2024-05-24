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
    public class ItemNotaRepository : IItemNotaRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public ItemNotaRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }

        public async Task<ItensNotum> Create(ItensNotum entity)
        {
            _context.ItensNota.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ItensNotum> Delete(ItensNotum entity)
        {
            _context.ItensNota.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

  
        public async Task<IEnumerable<ItensNotum>> GetAll() => await _context.ItensNota.AsNoTracking().ToListAsync();

     

        public async Task<ItensNotum> GetById(int itemNum, int idPro, int idNota, int idSec)
        {
            return await _context.ItensNota.FirstOrDefaultAsync(x => x.ItemNum == itemNum && x.IdPro == idPro && x.IdNota == idNota && x.IdSec == idSec);
        }

        public async Task<int> Update(ItensNotum entity)
        {
            _context.ItensNota.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
