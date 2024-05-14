﻿using AlmoxarifadoAPI.Models;
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

        public async Task<NotaFiscal> Create(NotaFiscal entity)
        {
            _context.NotaFiscals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NotaFiscal> Delete(NotaFiscal entity)
        {
            _context.NotaFiscals.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<NotaFiscal>> GetAll() => await _context.NotaFiscals.AsNoTracking().ToListAsync();


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
