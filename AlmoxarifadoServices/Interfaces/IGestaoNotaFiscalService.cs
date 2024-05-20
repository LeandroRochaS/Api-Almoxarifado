using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoNotaFiscalService
    {
        Task<NotaFiscalComItensGetDTO> CriarItens(
            List<NotaFiscalPostDTO> itens,
            NotaFiscal notaFiscal
        );
    }
}
