using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using AlmoxarifadoServices.ViewModels.Requisicao;
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
<<<<<<< HEAD
<<<<<<< Updated upstream
        Task<NotaFiscal> RegistroDeNotaFiscal(CreateNotaFiscalViewModel notaFiscal);
        Task<ItensNotum> RegistrarItemDeNotaFiscal(int id, CreateItemNotaFiscaViewModel itemFiscal);
=======
        Task<NotaFiscalComItensGetDTO> CriarItens(List<ItemNotaFiscalPostDTO> itens, NotaFiscal notaFiscal);
>>>>>>> Stashed changes
=======
        Task<GetNotaFiscalComItensViewModel> CriarItens(List<CreateItemNotaFiscalViewModel> itens, NotaFiscal notaFiscal);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
