using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;
    
 
        public GestaoRequisicaoService(IItemRequisicaoService itemRequisicaoService, IRequisicaoService requisicaoService, ISetorService setorService, IClienteService clienteService, ISecretariaService secretariaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _itemRequisicaoService = itemRequisicaoService;
        }
    
<<<<<<< HEAD
        public async Task<RequisicaoComItensGetDTO> CriarItens(List<ItemRequisicaoPostDTO> itens, RequisicaoGetDTO model)
        {
            try
            {
                foreach (ItemRequisicaoPostDTO item in itens)
=======
        public async Task<GetRequisicaoComItensViewModel> CriarItens(List<CreateItemRequisicaoViewModel> itens, Requisicao model)
        {
            try
            {
                foreach (CreateItemRequisicaoViewModel item in itens)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
                {
                    await _itemRequisicaoService.Create(model.IdReq, item);
                }

<<<<<<< HEAD
                var requisicaoGet = new RequisicaoComItensGetDTO
                {
                    Requisicao = new RequisicaoPostDTO
=======
                var requisicaoGet = new GetRequisicaoComItensViewModel
                {
                    Requisicaoo = new CreateRequisicaoViewModel
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
