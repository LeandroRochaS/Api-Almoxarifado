using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
using AlmoxarifadoServices.ViewModels.NotaFiscal;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
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
=======
        Task<NotaFiscal> GetById(int id);

        Task<NotaFiscal> Create(CreateNotaFiscalViewModel notaFiscal);

        Task<IEnumerable<NotaFiscal>> GetAll();
        Task<NotaFiscal> Delete(int id);

        Task<NotaFiscal> Update(int id, NotaFiscal notaFiscal);

        Task<NotaFiscal> AdicionarItem(NotaFiscal notaFiscal);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
