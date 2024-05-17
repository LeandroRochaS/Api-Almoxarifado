using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoNotaFiscalService : IGestaoNotaFiscalService
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly ISecretariaService _secretariaService;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IProdutoService _produtoService;
        private readonly IItemNotaService _itemNotaService;

        public GestaoNotaFiscalService(IFornecedorService fornecedorService, ISecretariaService secretariaService, INotaFiscalService notaFiscalService, IItemNotaService itemNotaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
            _notaFiscalService = notaFiscalService;
            _itemNotaService = itemNotaService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
        }


        public async Task<GetNotaFiscalComItensViewModel> CriarItens(List<CreateItemNotaFiscalViewModel> itens, NotaFiscal notaFiscal)
        {
            try
            {
                foreach (CreateItemNotaFiscalViewModel item in itens)
                {
                    await _itemNotaService.Create(notaFiscal.IdNota, item);
                }

                var notaFiscalGet = new GetNotaFiscalComItensViewModel
                {
                    NotaFiscal = new CreateNotaFiscalViewModel
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

                return notaFiscalGet;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar itens da nota fiscal.", ex);
            }
        }
    }
}
