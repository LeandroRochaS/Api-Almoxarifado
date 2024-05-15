using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Repositories
{
    public class RequisicaoRepository : IRequisicaoRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public RequisicaoRepository(xAlmoxarifadoContext context)
        {
            _context = context;
        }

        public async Task<Requisicao> Create(Requisicao entity)
        {
            _context.Requisicaos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Requisicao> Delete(Requisicao entity)
        {
            _context.Requisicaos.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Requisicao>> GetAll()
        {
            return await _context.Requisicaos.ToListAsync();
        }

        public async Task<Requisicao> GetById(int id)
        {
            return await _context.Requisicaos.FindAsync(id);
        }

        public async Task<Requisicao> Update(Requisicao entity)
        {
            _context.Requisicaos.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
