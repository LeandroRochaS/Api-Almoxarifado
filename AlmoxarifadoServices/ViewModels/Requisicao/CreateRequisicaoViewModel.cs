using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.Requisicao
{
    public class CreateRequisicaoViewModel
    {
        public int IdCli { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int IdSec { get; set; }
        public int IdSet { get; set; }
        public string? Observacao { get; set; }
    }
}
