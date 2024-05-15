using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using AlmoxarifadoServices.ViewModels.NotaFiscal;

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

        public GestaoNotaFiscalService(IFornecedorService fornecedorService, ISecretariaService secretariaService, INotaFiscalService notaFiscalService, IItemNotaService itemNotaService, IProdutoService produtoService, IEstoqueService estoqueServices)
        {
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
            _notaFiscalService = notaFiscalService;
            _itemNotaService = itemNotaService;
            _produtoService = produtoService;
            _estoqueService = estoqueServices;


        }

        public async Task<ItensNotum> RegistrarItemDeNotaFiscal(int id, CreateItemNotaFiscaViewModel itemFiscal)
        {
            try
            {
                var notaFiscal = await ExisteNotaFiscal(id);
                if (notaFiscal != null)
                {
                    if (await VerificarRelacionamentosItem(itemFiscal))
                    {

                        ItensNotum item = new ItensNotum
                        {
                            ItemNum = itemFiscal.ItemNum,
                            IdPro = itemFiscal.IdPro,
                            IdSec = itemFiscal.IdSec,
                            QtdPro = itemFiscal.QtdPro,
                            PreUnit = itemFiscal.PreUnit,
                            TotalItem = itemFiscal.QtdPro * itemFiscal.PreUnit,
                            EstLin = 0,
                            IdNota = id
                        };

                        var resultItem = await _itemNotaService.Create(item);
                        if(resultItem != null)
                        {
                            await _notaFiscalService.AdicionarItem(notaFiscal);
                            var resultEstoque = _estoqueService.AdicionarEstoque(itemFiscal.IdPro, itemFiscal.QtdPro);
                            
                            return item;
                        }
                    }

                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<NotaFiscal> RegistroDeNotaFiscal(CreateNotaFiscalViewModel notaFiscalView)
        {
            try
            {
                var resultVerificacao = await VerificarRelacionamentosNotaFiscal(notaFiscalView);
                if (resultVerificacao)
                {
                    NotaFiscal notaFiscal = new NotaFiscal
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


                    return await _notaFiscalService.CreateNotaFiscal(notaFiscal);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> VerificarRelacionamentosNotaFiscal(CreateNotaFiscalViewModel notaFiscal)
        {
            if (notaFiscal.IdFor == 0 || notaFiscal.IdSec == 0)
                throw new ArgumentException("Fornecedor ou Secretaria inválida");

            var fornecedor = await _fornecedorService.GetById(notaFiscal.IdFor);
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado");

            var secretaria = await _secretariaService.GetById(notaFiscal.IdSec);
            if (secretaria == null)
                throw new ArgumentException("Secretaria não encontrada");

            return true;
        }

        public async Task<bool> VerificarRelacionamentosItem(CreateItemNotaFiscaViewModel itemFiscal)
        {
            if (itemFiscal.IdSec == 0 || itemFiscal.IdPro == 0)
                throw new ArgumentException("Produto ou Secretaria inválido");

            var produto = await _produtoService.GetById(itemFiscal.IdPro);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            var secretaria = await _secretariaService.GetById(itemFiscal.IdSec);
            if (secretaria == null)
                throw new ArgumentException("Secretaria não encontrada");

            return true;
        }

        public async Task<NotaFiscal> ExisteNotaFiscal(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaFiscalById(id);
            if (notaFiscal == null)
                throw new ArgumentException("Nota Fiscal não encontrada");

            return notaFiscal;
        }


    }
}
