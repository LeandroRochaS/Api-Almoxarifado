using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoNotaFiscalService
    {
        Task<GetNotaFiscalComItensViewModel> CriarItens(List<CreateItemNotaFiscalViewModel> itens, NotaFiscal notaFiscal);
    }
}
