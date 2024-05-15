using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IGestaoRequisicaoService
    {
        Task<Requisicao> RegistrarRequisicao(CreateRequisicaoViewModel requisicao);

        Task<ItensReq> RegistrarItemRequisicao(int id, CreateItemRequisicaoViewModel itemReq);
    }
}
