using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.NotaFiscal
{
    public class CreateNotaFiscalViewModel
    {
        public int IdFor { get; set; }
        public int IdSec { get; set; }
        public string NumNota { get; set; } = null!;
        public int QtdItem { get; set; }
        public int Ano { get; set; }
        public int? Mes { get; set; }
        public int IdTipoNota { get; set; }
        public string? ObservacaoNota { get; set; }
    }
}
