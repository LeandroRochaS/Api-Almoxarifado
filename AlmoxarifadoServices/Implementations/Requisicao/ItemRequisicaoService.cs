﻿using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
using SeuProjeto.Services.Implementations;
using System.Transactions;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemRequisicaoService : IItemRequisicaoService
    {
        private readonly IItemRequisicaoRepository _itemRequisicaoRepository;
        private readonly MapperConfiguration configurationMapper;
        private readonly IRequisicaoService _requisicaoService;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;
        private readonly ISetorService _setorService;

        public ItemRequisicaoService(
            IItemRequisicaoRepository itemRequisicaoRepository,
            IRequisicaoService requisicaoService,
            IProdutoService produtoService,
            IEstoqueService estoqueService,
            ISetorService setorService
        )
        {
            _itemRequisicaoRepository = itemRequisicaoRepository;
            configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItensReq, ItemRequisicaoGetDTO>();
                cfg.CreateMap<ItemRequisicaoGetDTO, ItensReq>();
                cfg.CreateMap<ProdutoGetDTO, Produto>();
            });
            _mapper = configurationMapper.CreateMapper();
            _requisicaoService = requisicaoService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _setorService = setorService;
        }

        public async Task<ItemRequisicaoGetDTO> Create(
            int id,
            ItemRequisicaoPostDTO itemRequisicaoView
        )
        {

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (
                        await ExisteRequisicao(id)
                        && await VerificarRelacionamentosItemReq(id, itemRequisicaoView)
                    )
                    {
                        decimal totalItem = itemRequisicaoView.QtdPro * itemRequisicaoView.PreUnit;
                        var itemRequisicao = CriarItemRequisicao(id, itemRequisicaoView, totalItem);

                        if (
                            await _estoqueService.VerificarEstoqueSuficiente(
                                itemRequisicao.IdPro,
                                itemRequisicao.IdSec,
                                itemRequisicao.QtdPro
                            )
                        )
                        {
                            var resultItem = await _itemRequisicaoRepository.Create(itemRequisicao);
                            if (resultItem != null)
                            {
                                await AtualizarEstoque(
                                    itemRequisicao.IdPro,
                                    itemRequisicao.IdSec,
                                    itemRequisicaoView.QtdPro
                                );
                                await VerificarEstoqueMinimo(
                                    itemRequisicao.IdPro,
                                    itemRequisicao.IdSec,
                                    itemRequisicao.IdReq
                                );
                                await _requisicaoService.AdicionarItem(itemRequisicao.IdReq);
                                await _requisicaoService.AtualizarTotalReq(itemRequisicao.IdReq);
                                scope.Complete();
                                return _mapper.Map<ItemRequisicaoGetDTO>(resultItem);
                            }  
                            else
                            {
                                throw new InvalidOperationException("Error ao tentar criar o item, verifique os dados e tente novamente.");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                "Quantidade de produto em estoque insuficiente"
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
              
                return null;
            }

        }

        public async Task<ItemRequisicaoGetDTO> Delete(KeyItemRequisicaoDTO keys)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(keys.NumItem, keys.IdProduto, keys.IdRequisicao, keys.IdSecretaria);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }
            var result = await _itemRequisicaoRepository.Delete(itemRequisicao);
            await _requisicaoService.AtualizarTotalReq(itemRequisicao.IdReq);
            await _requisicaoService.RemoverItem(itemRequisicao.IdReq);
            return _mapper.Map<ItemRequisicaoGetDTO>(result);
        }

        public async Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll()
        {
            var items = await _itemRequisicaoRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemRequisicaoGetDTO>>(items);
        }

        public async Task<ItemRequisicaoGetDTO> GetByIds(KeyItemRequisicaoDTO keys)
        {
            var item = await _itemRequisicaoRepository.GetById(keys.NumItem, keys.IdProduto, keys.IdRequisicao, keys.IdSecretaria);
            return _mapper.Map<ItemRequisicaoGetDTO>(item);
        }

        public async Task<ItemRequisicaoGetDTO> Update(KeyItemRequisicaoDTO keys, ItemRequisicaoPutDTO entity)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(keys.NumItem, keys.IdProduto, keys.IdRequisicao, keys.IdSecretaria);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

            itemRequisicao.QtdPro = entity.QtdPro;
            itemRequisicao.PreUnit = entity.PreUnit ?? itemRequisicao.PreUnit;
            itemRequisicao.TotalItem = itemRequisicao.QtdPro * (itemRequisicao.PreUnit ?? 0);
            itemRequisicao.TotalReal = itemRequisicao.QtdPro * (itemRequisicao.PreUnit ?? 0);

            var result = await _itemRequisicaoRepository.Update(itemRequisicao);
            return _mapper.Map<ItemRequisicaoGetDTO>(itemRequisicao);
        }

        private async Task VerificarEstoqueMinimo(int idPro, int idSec, int idReq)
        {
            var produto = _mapper.Map<Produto>(await _produtoService.GetById(idPro));
            var estoqueProduto = await _estoqueService.GetById(idPro, idSec);
            if (produto.VerificarEstoqueMinimo(estoqueProduto.QtdPro))
            {
                LogEstoqueCriticoService.CriarOuAtualizarLogCSV(
                    new LogEstoqueMinimo
                    {
                        IdProduto = idPro,
                        IdRequisicao = idReq,
                        IdSecretaria = idSec,
                        DataRegistro = DateTime.Now,
                        QuantidadeAtual = estoqueProduto.QtdPro
                    }
                );
            }
        }

        private async Task<bool> VerificarRelacionamentosItemReq(
            int Idreq,
            ItemRequisicaoPostDTO itemReq
        )
        {
            return itemReq.IdPro != 0
                && Idreq != 0
                && itemReq.IdSec != 0
                && await _requisicaoService.GetById(Idreq) != null
                && await _produtoService.GetById(itemReq.IdPro) != null
                && await _setorService.GetById(itemReq.IdSec) != null;
        }

        private async Task<bool> ExisteRequisicao(int id)
        {
            return await _requisicaoService.GetById(id) != null;
        }

        private ItensReq CriarItemRequisicao(
            int id,
            ItemRequisicaoPostDTO itemRequisicaoView,
            decimal totalItem
        )
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

        private async Task AtualizarEstoque(int IdPro, int IdSec, decimal quantidadeSaida)
        {
            await _estoqueService.AtualizarEstoque(IdPro, IdSec, quantidadeSaida, false);
        }

     
    }
}
