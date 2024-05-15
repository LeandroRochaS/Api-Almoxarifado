using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;


namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;
        private readonly IRequisicaoService _requisicaoService;
        private readonly ISetorService _setorService;
        private readonly IClienteService _clienteService;
        private readonly ISecretariaService _secretariaService;
        private readonly IProdutoService _produtoService;

        public GestaoRequisicaoService(IItemRequisicaoService itemRequisicaoService, IRequisicaoService requisicaoService, ISetorService setorService, IClienteService clienteService, ISecretariaService secretariaService, IProdutoService produtoService)
        {
            _itemRequisicaoService = itemRequisicaoService;
            _requisicaoService = requisicaoService;
            _setorService = setorService;
            _clienteService = clienteService;
            _secretariaService = secretariaService;
            _produtoService = produtoService;
        }

        public async Task<ItensReq> RegistrarItemRequisicao(int id, CreateItemRequisicaoViewModel itemRequisicaoView)
        {
            try
            {
                if(await ExisteRequisicao(id))
                {
                    if(await VerificarRelacionamentosItemReq(itemRequisicaoView))
                    {
                        decimal totalItem = itemRequisicaoView.QtdPro * itemRequisicaoView.PreUnit;
                        ItensReq itemRequisicao = new ItensReq
                        {
                            IdPro = itemRequisicaoView.IdPro,
                            IdReq = itemRequisicaoView.IdReq,
                            IdSec = itemRequisicaoView.IdSec,
                            PreUnit = itemRequisicaoView.PreUnit,
                            NumItem = itemRequisicaoView.NumItem,
                            QtdPro = itemRequisicaoView.QtdPro,
                            TotalItem = totalItem,
                            TotalReal = totalItem
                        };

                        return await _itemRequisicaoService.Create(itemRequisicao);
                    }
                }
            } catch
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public async Task<Requisicao> RegistrarRequisicao(CreateRequisicaoViewModel requisicaoView)
        {
            try
            {
                if(await VerificarRelacionamentosRequisicao(requisicaoView))
                {
                    Requisicao requisicao = new Requisicao
                    {
                        Ano = requisicaoView.Ano,
                        DataReq = DateTime.Now,
                        IdCli = requisicaoView.IdCli,
                        IdSec = requisicaoView.IdSec,
                        IdSet = requisicaoView.IdSet,
                        Mes = requisicaoView.Mes,
                        Observacao = requisicaoView.Observacao,
                        TotalReq = 0,
                        QtdIten = 0
                    };

                    return await _requisicaoService.Create(requisicao);
                }
            } catch
            {
                throw new NotImplementedException();
            }

            return null;
        }


        private async Task<bool> VerificarRelacionamentosRequisicao(CreateRequisicaoViewModel requisicao)
        {
            if(requisicao.IdCli == 0 || requisicao.IdSec == 0 || requisicao.IdSet == 0)
                return false;
            
            if(await _clienteService.GetById(requisicao.IdCli) == null || await _setorService.GetById(requisicao.IdSet) == null || await _secretariaService.GetById(requisicao.IdSec) == null)
                return false;

            return true;
        }

        private async Task<bool> VerificarRelacionamentosItemReq(CreateItemRequisicaoViewModel itemReq)
        {
            if(itemReq.IdPro == 0 || itemReq.IdReq == 0 || itemReq.IdSec == 0)
                return false;
            if(await _requisicaoService.GetById(itemReq.IdReq) == null || await _produtoService.GetById(itemReq.IdPro) == null || await _setorService.GetById(itemReq.IdSec) == null)
                return false;

            return true;
        }

        private async Task<bool> ExisteRequisicao(int id)
        {
            if(await _requisicaoService.GetById(id) == null)
                return false;

            return true;
        }
    } 
}
