using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface INotaFiscalService
    {
        Task<NotaFiscal> GetNotaFiscalById(int id);

        Task<NotaFiscal> CreateNotaFiscal(NotaFiscal notaFiscal);
    }
}
