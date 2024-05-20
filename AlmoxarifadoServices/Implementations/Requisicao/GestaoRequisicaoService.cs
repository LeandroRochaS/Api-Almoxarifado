using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;
    
 
        public GestaoRequisicaoService(IItemRequisicaoService itemRequisicaoService, IRequisicaoService requisicaoService, ISetorService setorService, IClienteService clienteService, ISecretariaService secretariaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _itemRequisicaoService = itemRequisicaoService;
        }
    
        public async Task<RequisicaoComItensGetDTO> CriarItens(List<ItemRequisicaoPostDTO> itens, RequisicaoGetDTO model)
        {
            try
            {
                foreach (ItemRequisicaoPostDTO item in itens)
                {
                    await _itemRequisicaoService.Create(model.IdReq, item);
                }

                var requisicaoGet = new RequisicaoComItensGetDTO
                {
                    Requisicao = new RequisicaoPostDTO
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
