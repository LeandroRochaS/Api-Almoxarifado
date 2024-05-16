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
        Task<NotaFiscal> GetById(int id);

        Task<NotaFiscal> Create(NotaFiscal notaFiscal);

        Task<IEnumerable<NotaFiscal>> GetAll();
        Task<NotaFiscal> Delete(int id);

        Task<NotaFiscal> Update(int id, NotaFiscal notaFiscal);

        Task<NotaFiscal> AdicionarItem(NotaFiscal notaFiscal);
    }
}
