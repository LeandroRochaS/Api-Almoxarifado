using System.Transactions;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;

        public GestaoRequisicaoService(
            IItemRequisicaoService itemRequisicaoService
        )
        {
            _itemRequisicaoService = itemRequisicaoService;
        }
        public async Task<RequisicaoComItensGetDTO> CriarItens(
            List<ItemRequisicaoPostDTO> itens,
            RequisicaoGetDTO model
        )
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (itens == null || !itens.Any())
                    {
                        throw new ArgumentException("A lista de itens está vazia ou nula.");
                    }

                    foreach (ItemRequisicaoPostDTO item in itens)
                    {
                        if (item == null)
                        {
                            throw new ArgumentException("Um ou mais itens na lista de itens são nulos.");
                        }

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

                    scope.Complete();
                    return requisicaoGet;
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Erro ao criar itens: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao criar itens: {ex.Message}");
                }
            }
        }

    }
}
