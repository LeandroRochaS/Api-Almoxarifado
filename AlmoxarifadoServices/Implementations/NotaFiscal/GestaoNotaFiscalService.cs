using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using System.Transactions;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoNotaFiscalService : IGestaoNotaFiscalService
    {
        private readonly IItemNotaService _itemNotaService;

        public GestaoNotaFiscalService(
            IItemNotaService itemNotaService
        )
        {
            _itemNotaService = itemNotaService;
        }

        public async Task<NotaFiscalComItensGetDTO> CriarItens(
        List<ItemNotaFiscalPostDTO> itens,
        NotaFiscal notaFiscal
            )
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (ItemNotaFiscalPostDTO item in itens)
                    {
                        await _itemNotaService.Create(notaFiscal.IdNota, item);
                    }

                    var notaFiscalGet = new NotaFiscalComItensGetDTO
                    {
                        NotaFiscal = new NotaFiscalPostDTO
                        {
                            IdTipoNota = notaFiscal.IdTipoNota,
                            IdFor = (int)notaFiscal.IdFor,
                            IdSec = notaFiscal.IdSec,
                            QtdItem = notaFiscal.QtdItem,
                            Ano = notaFiscal.Ano,
                            Mes = notaFiscal.Mes,
                            NumNota = notaFiscal.NumNota,
                        },
                        Itens = itens
                    };

                    scope.Complete(); 
                    return notaFiscalGet;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Erro ao criar itens da nota fiscal.", ex);
                }
            }
        }

    }
}
