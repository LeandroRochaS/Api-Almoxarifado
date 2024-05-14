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
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly xAlmoxarifadoContext _context;

        public ProdutoRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }

        public async Task<Produto> Create(Produto entity)
        {
            _context.Produtos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Produto> Delete(Produto entity)
        {
            _context.Produtos.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Produto>> GetAll() => await _context.Produtos.ToListAsync();


        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos.Include(x => x.ItensNota).FirstOrDefaultAsync(x => x.IdPro == id);
        }

        public async Task<Produto> Update(Produto entity)
        {
            _context.Produtos.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
    }
