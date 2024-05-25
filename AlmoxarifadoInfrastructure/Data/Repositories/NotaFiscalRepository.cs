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
    public class NotaFiscalRepository : INotaFiscalRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public NotaFiscalRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }

        public async Task<NotaFiscal> AtualizarValorDaNota(NotaFiscal notaFiscald)
        {
            decimal? total = 0;
            NotaFiscal notaFiscal = await _context.NotaFiscals.Include(x => x.ItensNota).FirstOrDefaultAsync(x => x.IdNota == notaFiscald.IdNota);
            var listItens = notaFiscal.ItensNota;
            foreach(ItensNotum item in listItens)
            {
                total += item.TotalItem;
            }

            notaFiscal.ValorNota = (decimal)total;
            return await Update(notaFiscal);
        }

        public async Task<NotaFiscal> Create(NotaFiscal entity)
        {
            _context.NotaFiscals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NotaFiscal> Delete(NotaFiscal entity)
        {
            try
            {
                _context.NotaFiscals.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            } catch
            {
                throw new DbUpdateException("Hà Itens vinculados a nota fiscal");
            }
          
        }

        public async Task<IEnumerable<NotaFiscal>> GetAll() => await _context.NotaFiscals.ToListAsync();


        public async Task<NotaFiscal> GetById(int id)
        {
            return await _context.NotaFiscals.FirstOrDefaultAsync(x => x.IdNota == id);
        }

        public async Task<NotaFiscal> Update(NotaFiscal entity)
        {
            _context.NotaFiscals.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
