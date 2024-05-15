using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;

        public NotaFiscalService(INotaFiscalRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotaFiscal> CreateNotaFiscal(NotaFiscal notaFiscal)
        {
            return await _repository.Create(notaFiscal);
        }

        public async Task<NotaFiscal> GetNotaFiscalById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<NotaFiscal> AdicionarItem(NotaFiscal notaFiscal)
        {
            var notaFiscalExistente = await _repository.GetById(notaFiscal.IdNota);
            if (notaFiscalExistente == null)
            {
                throw new ArgumentException("Nota fiscal não encontrada.");
            }
            notaFiscal.QtdItem += 1;
            return await _repository.Update(notaFiscal);
        }
    }
}
