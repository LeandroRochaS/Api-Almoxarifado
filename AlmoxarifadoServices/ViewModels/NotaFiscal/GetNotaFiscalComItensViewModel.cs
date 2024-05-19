using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.NotaFiscal
{
    public class GetNotaFiscalComItensViewModel
    {
        public CreateNotaFiscalViewModel NotaFiscal { get; set; }
        public List<CreateItemNotaFiscalViewModel> Itens { get; set; }
    }
}
