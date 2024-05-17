using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemNotaService : IItemNotaService
    {
        private readonly IItemNotaRepository _repository;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IProdutoService _produtoService;
        private readonly ISecretariaService _secretariaService;
        private readonly IEstoqueService _estoqueService;


        public ItemNotaService(IItemNotaRepository repository, INotaFiscalService notaFiscalService, IProdutoService produtoService, ISecretariaService secretariaService, IEstoqueService estoqueService)
        {
            _repository = repository;
            _notaFiscalService = notaFiscalService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _secretariaService = secretariaService;
        }

        public async Task<ItensNotum> Create(int idNotaFiscal, CreateItemNotaFiscalViewModel itemFiscal)
        {
            try
            {
                var notaFiscal = await ObterNotaFiscalPorId(idNotaFiscal);
                if (notaFiscal != null)
                {
                    await VerificarRelacionamentosItem(itemFiscal);

                    var item = CriarItemNotaFiscal(itemFiscal, idNotaFiscal);

                    var resultItem = await _repository.Create(item);
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


        public async Task<ItensNotum> Delete(int id)
        {
            var item = await _repository.GetById(id);
            if (item != null)
            {
                await _repository.Delete(item);
            }
            return null;
        }

        public async Task<IEnumerable<ItensNotum>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ItensNotum> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ItensNotum> Update(int id, ItensNotum entity)
        {
            var ItemNota = await _repository.GetById(id);
            if (ItemNota == null)
                return null;

            return await _repository.Update(entity);
        }

        private async Task<bool> VerificarRelacionamentosItem(CreateItemNotaFiscalViewModel itemFiscal)
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

        private async Task<Secretarium> ObterSecretariaPorId(int id)
        {
            return await _secretariaService.GetById(id);
        }

        private async Task<Produto> ObterProdutoPorId(int id)
        {
            return await _produtoService.GetById(id);
        }
        private ItensNotum CriarItemNotaFiscal(CreateItemNotaFiscalViewModel itemFiscal, int idNotaFiscal)
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



        private async Task AtualizarEstoque(int IdPro, decimal quantidadeSaida)
        {
            await _estoqueService.AdicionarEstoque(IdPro, quantidadeSaida);
        }

    }
}
