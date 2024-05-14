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
    public class FornecedorRepository : IFornecedorRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public FornecedorRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }

        public async Task<Fornecedor> Create(Fornecedor entity)
        {
            _context.Fornecedors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Fornecedor> Delete(Fornecedor entity)
        {
            _context.Fornecedors.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Fornecedor>> GetAll() => await _context.Fornecedors.AsNoTracking().ToListAsync();


        public async Task<Fornecedor> GetById(int id)
        {
            return await _context.Fornecedors.FirstOrDefaultAsync(x => x.IdFor == id);
        }

        public async Task<Fornecedor> Update(Fornecedor entity)
        {
            _context.Fornecedors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
