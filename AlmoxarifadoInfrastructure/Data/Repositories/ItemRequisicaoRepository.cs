using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Repositories
{
    public class ItemRequisicaoRepository : IItemRequisicaoRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public ItemRequisicaoRepository(xAlmoxarifadoContext context)
        {
            _context = context;
        }

        public async Task<ItensReq> Create(ItensReq entity)
        {
            _context.ItensReqs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ItensReq> Delete(ItensReq entity)
        {
            _context.ItensReqs.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<IEnumerable<ItensReq>> GetAll()
        {
            return await _context.ItensReqs.ToListAsync();
        }



        public async Task<ItensReq> GetById(int itemNum, int idPro, int idReq, int idSec)
        {
            return await _context.ItensReqs.FirstOrDefaultAsync(x => x.NumItem == itemNum && x.IdPro == idPro && x.IdReq == idReq && x.IdSec == idSec);
        }

        public async Task<int> Update(ItensReq entity)
        {
            _context.ItensReqs.Update(entity);
            return await _context.SaveChangesAsync(); 
        }
 
    }
}
