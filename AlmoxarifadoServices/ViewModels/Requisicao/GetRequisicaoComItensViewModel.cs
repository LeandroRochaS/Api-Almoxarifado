using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.Requisicao
{
    public class GetRequisicaoComItensViewModel
    {
        public CreateRequisicaoViewModel Requisicaoo { get; set; }
        public List<CreateItemRequisicaoViewModel> Itens { get; set; }
    }
}
