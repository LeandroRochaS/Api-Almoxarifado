using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        public Task<ItensReq> RegistrarItemRequisicao(ItensReq itemRequisicao)
        {
            throw new NotImplementedException();
        }

        public Task<Requisicao> RegistrarRequisicao(Requisicao requisicao)
        {
            throw new NotImplementedException();
        }
    }
}
