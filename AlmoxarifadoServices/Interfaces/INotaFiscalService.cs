using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using AlmoxarifadoServices.DTO;
>>>>>>> Stashed changes

namespace AlmoxarifadoServices.Interfaces
{
    public interface INotaFiscalService
    {
<<<<<<< Updated upstream
        Task<NotaFiscal> GetNotaFiscalById(int id);

        Task<NotaFiscal> CreateNotaFiscal(NotaFiscal notaFiscal);
=======
        Task<NotaFiscalGetDTO> GetById(int id);

        Task<NotaFiscal> Create(NotaFiscalPostDTO notaFiscal);

        Task<IEnumerable<NotaFiscalGetDTO>> GetAll();
        Task<NotaFiscalGetDTO> Delete(int id);

        Task<NotaFiscalGetDTO> Update(int id, NotaFiscalPutDTO notaFiscal);

        Task<NotaFiscalGetDTO> AdicionarItem(NotaFiscal notaFiscal);
>>>>>>> Stashed changes
    }
}
