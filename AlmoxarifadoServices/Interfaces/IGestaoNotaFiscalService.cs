using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
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
    public interface IGestaoNotaFiscalService
    {
<<<<<<< Updated upstream
        Task<NotaFiscal> RegistroDeNotaFiscal(CreateNotaFiscalViewModel notaFiscal);
        Task<ItensNotum> RegistrarItemDeNotaFiscal(int id, CreateItemNotaFiscaViewModel itemFiscal);
=======
        Task<NotaFiscalComItensGetDTO> CriarItens(List<ItemNotaFiscalPostDTO> itens, NotaFiscal notaFiscal);
>>>>>>> Stashed changes
    }
}
