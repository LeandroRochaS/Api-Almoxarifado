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
    public class SecretariaRepository : ISecretariaRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public SecretariaRepository(xAlmoxarifadoContext context)
        {
            _context = context;
        }

        public async Task<Secretarium> Create(Secretarium entity)
        {
            _context.Secretaria.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Secretarium> Delete(Secretarium entity)
        {
            _context.Secretaria.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Secretarium>> GetAll() => await _context.Secretaria.AsNoTracking().ToListAsync();

        public async Task<Secretarium> GetById(int id)
        {
            return await _context.Secretaria.FirstOrDefaultAsync(x => x.IdSec == id);
        }

        public async Task<Secretarium> Update(Secretarium entity)
        {
            _context.Secretaria.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
