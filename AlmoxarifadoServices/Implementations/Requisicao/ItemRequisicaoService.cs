using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemRequisicaoService : IItemRequisicaoService
    {
        private readonly IItemRequisicaoRepository _itemRequisicaoRepository;
        private readonly IRequisicaoService _requisicaoService;
        private readonly IProdutoService _produtoService;
        private readonly IEstoqueService _estoqueService;
        private readonly ISetorService _setorService;


        public ItemRequisicaoService(IItemRequisicaoRepository itemRequisicaoRepository, IRequisicaoService requisicaoService, IProdutoService produtoService, IEstoqueService estoqueService, ISetorService setorService)
        {
            _itemRequisicaoRepository = itemRequisicaoRepository;
            _requisicaoService = requisicaoService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _setorService = setorService;

        }

        public async Task<ItensReq> Create(int id, CreateItemRequisicaoViewModel itemRequisicaoView)
        {

            try
            {
                if (await ExisteRequisicao(id))
                {
                    if (await VerificarRelacionamentosItemReq(id, itemRequisicaoView))
                    {
                        decimal totalItem = itemRequisicaoView.QtdPro * itemRequisicaoView.PreUnit;
                        ItensReq itemRequisicao = CriarItemRequisicao(id, itemRequisicaoView, totalItem);

                        if (await VerificarEstoqueSuficiente(itemRequisicao.IdPro, itemRequisicao.QtdPro))
                        {
                            var resultItem = await _itemRequisicaoRepository.Create(itemRequisicao);
                            if (resultItem != null)
                            {
                                await AtualizarEstoque(itemRequisicao.IdPro, itemRequisicaoView.QtdPro);
                                await VerificarEstoqueMinimo(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.IdReq);
                                return resultItem;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Quantidade de produto em estoque insuficiente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
        public async Task<ItensReq> Delete(int id)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

            return await _itemRequisicaoRepository.Delete(itemRequisicao);
        }

        public async Task<IEnumerable<ItensReq>> GetAll()
        {
            return await _itemRequisicaoRepository.GetAll();
        }

        public async Task<ItensReq> GetById(int id)
        {
            return await _itemRequisicaoRepository.GetById(id);
        }

        public async Task<ItensReq> Update(int id, ItensReq entity)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

            return await _itemRequisicaoRepository.Update(entity);
        }

        private async Task VerificarEstoqueMinimo(int idPro, int idSec, int idReq)
        {
            var produto = await _produtoService.GetById(idPro);
            var estoqueProduto = await _estoqueService.GetById(idPro);
            if (estoqueProduto.QtdPro <= produto.EstoqueMin)
            {
                LogEstoqueCriticoService.CriarLogCSV(new LogEstoqueMinimo
                {
                    IdProduto = idPro,
                    IdRequisicao = idReq,
                    IdSecretaria = idSec,
                    DataRegistro = DateTime.Now,
                    QuantidadeAtual = estoqueProduto.QtdPro
                });

            }
        }

        private async Task<bool> VerificarRelacionamentosItemReq(int Idreq, CreateItemRequisicaoViewModel itemReq)
        {
            return itemReq.IdPro != 0 &&
                   Idreq != 0 &&
                   itemReq.IdSec != 0 &&
                   await _requisicaoService.GetById(Idreq) != null &&
                   await _produtoService.GetById(itemReq.IdPro) != null &&
                   await _setorService.GetById(itemReq.IdSec) != null;
        }

        private async Task<bool> ExisteRequisicao(int id)
        {
            return await _requisicaoService.GetById(id) != null;
        }

        private ItensReq CriarItemRequisicao(int id, CreateItemRequisicaoViewModel itemRequisicaoView, decimal totalItem)
        {
            return new ItensReq
            {
                IdPro = itemRequisicaoView.IdPro,
                IdReq = id,
                IdSec = itemRequisicaoView.IdSec,
                PreUnit = itemRequisicaoView.PreUnit,
                NumItem = itemRequisicaoView.NumItem,
                QtdPro = itemRequisicaoView.QtdPro,
                TotalItem = totalItem,
                TotalReal = totalItem
            };
        }

        private async Task AtualizarEstoque(int IdPro, decimal quantidadeSaida)
        {
            await _estoqueService.RemoverEstoque(IdPro, quantidadeSaida);
        }

        public async Task<bool> VerificarEstoqueSuficiente(int IdPro, decimal quantidadeSaida)
        {
            var produto = await _produtoService.GetById(IdPro);
            if (produto == null)
                return false;

            var estoque = await _estoqueService.GetById(produto.IdPro);
            if (estoque == null)
                return false;

            return quantidadeSaida <= estoque.QtdPro;
        }


    }
}
