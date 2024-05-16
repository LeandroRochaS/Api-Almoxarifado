using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
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

        public async Task<ItensNotum> RegistrarItemDeNotaFiscal(int idNotaFiscal, CreateItemNotaFiscaViewModel itemFiscal)
        {
            try
            {
                var notaFiscal = await ObterNotaFiscalPorId(idNotaFiscal);
                if (notaFiscal != null)
                {
                    await VerificarRelacionamentosItem(itemFiscal);

                    var item = CriarItemNotaFiscal(itemFiscal, idNotaFiscal);

                    var resultItem = await _itemNotaService.Create(item);
                    if (resultItem != null)
                    {
                        await AtualizarEstoque(itemFiscal.IdPro, itemFiscal.QtdPro);
                        await _notaFiscalService.AdicionarItem(notaFiscal);
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Logar a exceção ou tratar conforme necessário
                throw ex;
            }
        }

        public async Task<NotaFiscal> RegistroDeNotaFiscal(CreateNotaFiscalViewModel notaFiscalView)
        {
            try
            {
                await VerificarRelacionamentosNotaFiscal(notaFiscalView);

                var notaFiscal = CriarNotaFiscal(notaFiscalView);

                return await _notaFiscalService.Create(notaFiscal);
            }
            catch (Exception ex)
            {
                // Logar a exceção ou tratar conforme necessário
                throw ex;
            }
        }

        private async Task<bool> VerificarRelacionamentosNotaFiscal(CreateNotaFiscalViewModel notaFiscal)
        {
            if (notaFiscal.IdFor == 0 || notaFiscal.IdSec == 0)
                throw new ArgumentException("Fornecedor ou Secretaria inválida");

            var fornecedor = await ObterFornecedorPorId(notaFiscal.IdFor);
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado");

            var secretaria = await ObterSecretariaPorId(notaFiscal.IdSec);
            if (secretaria == null)
                throw new ArgumentException("Secretaria não encontrada");

            return true;
        }

        private async Task<bool> VerificarRelacionamentosItem(CreateItemNotaFiscaViewModel itemFiscal)
        {
            if (itemFiscal.IdSec == 0 || itemFiscal.IdPro == 0)
                throw new ArgumentException("Produto ou Secretaria inválido");

            var produto = await ObterProdutoPorId(itemFiscal.IdPro);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            var secretaria = await ObterSecretariaPorId(itemFiscal.IdSec);
            if (secretaria == null)
                throw new ArgumentException("Secretaria não encontrada");

            return true;
        }

        private async Task<NotaFiscal> ObterNotaFiscalPorId(int id)
        {
            var notaFiscal = await _notaFiscalService.GetById(id);
            if (notaFiscal == null)
                throw new ArgumentException("Nota Fiscal não encontrada");

            return notaFiscal;
        }

        private async Task<Fornecedor> ObterFornecedorPorId(int id)
        {
            return await _fornecedorService.GetById(id);
        }

        private async Task<Secretarium> ObterSecretariaPorId(int id)
        {
            return await _secretariaService.GetById(id);
        }

        private async Task<Produto> ObterProdutoPorId(int id)
        {
            return await _produtoService.GetById(id);
        }

        private ItensNotum CriarItemNotaFiscal(CreateItemNotaFiscaViewModel itemFiscal, int idNotaFiscal)
        {
            return new ItensNotum
            {
                ItemNum = itemFiscal.ItemNum,
                IdPro = itemFiscal.IdPro,
                IdSec = itemFiscal.IdSec,
                QtdPro = itemFiscal.QtdPro,
                PreUnit = itemFiscal.PreUnit,
                TotalItem = itemFiscal.QtdPro * itemFiscal.PreUnit,
                EstLin = 0,
                IdNota = idNotaFiscal
            };
        }

        private NotaFiscal CriarNotaFiscal(CreateNotaFiscalViewModel notaFiscalView)
        {
            return new NotaFiscal
            {
                Ano = notaFiscalView.Ano,
                DataNota = DateTime.Now,
                EmpenhoNum = 0,
                Icms = 0,
                IdFor = notaFiscalView.IdFor,
                IdSec = notaFiscalView.IdSec,
                Iss = 0,
                Mes = notaFiscalView.Mes,
                IdTipoNota = notaFiscalView.IdTipoNota,
                ObservacaoNota = notaFiscalView.ObservacaoNota,
                QtdItem = notaFiscalView.QtdItem,
                NumNota = notaFiscalView.NumNota,
                ValorNota = 0
            };
        }

        private async Task AtualizarEstoque(int IdPro, decimal quantidadeSaida)
        {
            await _estoqueService.AdicionarEstoque(IdPro, quantidadeSaida);
        }
    }
}
