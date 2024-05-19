using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlmoxarifadoInfrastructure.Data.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public EstoqueRepository(xAlmoxarifadoContext context)
        {
            _context = context;
        }

        public async Task<Estoque> Create(Estoque entity)
        {
            _context.Estoques.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Estoque> Delete(Estoque entity)
        {
            _context.Estoques.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Estoque>> GetAll()
        {
            return await _context.Estoques.ToListAsync();
        }

        public async Task<Estoque> GetById(int id, int idSec)
        {
            return await _context.Estoques.FirstOrDefaultAsync(x => x.IdPro == id && x.IdSec == idSec);
        }

        public async Task<Estoque> Update(Estoque entity)
        {
            _context.Estoques.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
