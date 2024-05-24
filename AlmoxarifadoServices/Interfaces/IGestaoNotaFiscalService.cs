using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoNotaFiscalService
    {
        Task<NotaFiscalComItensGetDTO> CriarItens(
            List<ItemNotaFiscalPostDTO> itens,
            NotaFiscal notaFiscal
        );
    }
}
