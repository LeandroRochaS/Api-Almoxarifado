using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;
    
 
        public GestaoRequisicaoService(IItemRequisicaoService itemRequisicaoService, IRequisicaoService requisicaoService, ISetorService setorService, IClienteService clienteService, ISecretariaService secretariaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _itemRequisicaoService = itemRequisicaoService;
        }
    
        public async Task<GetRequisicaoComItensViewModel> CriarItens(List<CreateItemRequisicaoViewModel> itens, Requisicao model)
        {
            try
            {
                foreach (CreateItemRequisicaoViewModel item in itens)
                {
                    await _itemRequisicaoService.Create(model.IdReq, item);
                }

                var requisicaoGet = new GetRequisicaoComItensViewModel
                {
                    Requisicaoo = new CreateRequisicaoViewModel
                    {
                        IdSec = model.IdSec,
                        IdCli = model.IdCli,
                        IdSet = (int)model.IdSet,
                        Ano = model.Ano,
                        Mes = model.Mes,
                        Observacao = model.Observacao,
                    },
                    Itens = itens
                };

                return requisicaoGet;
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
