using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoNotaFiscalService : IGestaoNotaFiscalService
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly ISecretariaService _secretariaService;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IProdutoService _produtoService;
        private readonly IItemNotaService _itemNotaService;
        private readonly IEstoqueService _estoqueService;
        public GestaoNotaFiscalService(IFornecedorService fornecedorService, ISecretariaService secretariaService, INotaFiscalService notaFiscalService, IItemNotaService itemNotaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
            _notaFiscalService = notaFiscalService;
            _itemNotaService = itemNotaService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
        }


<<<<<<< HEAD
        public async Task<NotaFiscalComItensGetDTO> CriarItens(List<ItemNotaFiscalPostDTO> itens, NotaFiscal notaFiscal)
        {
            try
            {
                foreach (ItemNotaFiscalPostDTO item in itens)
=======
        public async Task<GetNotaFiscalComItensViewModel> CriarItens(List<CreateItemNotaFiscalViewModel> itens, NotaFiscal notaFiscal)
        {
            try
            {
                foreach (CreateItemNotaFiscalViewModel item in itens)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
                {
                    await _itemNotaService.Create(notaFiscal.IdNota, item);
                }

<<<<<<< HEAD
                var notaFiscalGet = new NotaFiscalComItensGetDTO
                {
                    NotaFiscal = new NotaFiscalPostDTO
=======
                var notaFiscalGet = new GetNotaFiscalComItensViewModel
                {
                    NotaFiscal = new CreateNotaFiscalViewModel
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
