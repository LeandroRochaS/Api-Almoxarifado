using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalGetDTO> GetById(int id);

        Task<NotaFiscal> Create(NotaFiscalPostDTO notaFiscal);

        Task<IEnumerable<NotaFiscalGetDTO>> GetAll();
        Task<NotaFiscalGetDTO> Delete(int id);

        Task<NotaFiscalGetDTO> Update(int id, NotaFiscalPutDTO notaFiscal);

        Task<NotaFiscalGetDTO> AdicionarItem(NotaFiscal notaFiscal);

        Task<NotaFiscalGetDTO> RemoverItem(NotaFiscal notaFiscal);
        Task<NotaFiscalGetDTO> AtualizarValorTotal(int idNota);


    }
}
