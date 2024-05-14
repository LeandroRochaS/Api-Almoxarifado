using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoNotaFiscalService
    {
        Task<NotaFiscal> RegistroDeNotaFiscal(CreateNotaFiscalViewModel notaFiscal);
        Task<ItensNotum> RegistrarItemDeNotaFiscal(int id, CreateItemNotaFiscaViewModel itemFiscal);
    }
}
