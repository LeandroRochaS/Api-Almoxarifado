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

        public async Task<ItensReq> GetById(int id)
        {
            return await _context.ItensReqs.FindAsync(id);
        }

        public async Task<ItensReq> Update(ItensReq entity)
        {
            _context.ItensReqs.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
