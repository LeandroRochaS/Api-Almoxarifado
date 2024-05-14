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
    public class ClienteRepository : IClienteRepository
    {
        private readonly xAlmoxarifadoContext _context;

        public ClienteRepository(xAlmoxarifadoContext pContext)
        {
            _context = pContext;
        }
        public async Task<Cliente> GetById(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.IdCli == id);
        }
    }
}
