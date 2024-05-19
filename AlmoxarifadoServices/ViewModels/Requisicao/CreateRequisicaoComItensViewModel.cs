using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.Requisicao
{
    public class CreateRequisicaoComItensViewModel
    {
        public CreateRequisicaoViewModel Requisicao { get; set; }
        public List<CreateItemRequisicaoViewModel> Itens { get; set; }
    }
}
