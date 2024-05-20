using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using System.Collections.Generic;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoServices.Implementations
{
    public class ItemRequisicaoService : IItemRequisicaoService
    {
        private readonly IItemRequisicaoRepository _itemRequisicaoRepository;
<<<<<<< HEAD
        private readonly MapperConfiguration configurationMapper;
        private readonly IRequisicaoService _requisicaoService;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;
        private readonly ISetorService _setorService;

        public ItemRequisicaoService(IItemRequisicaoRepository itemRequisicaoRepository, IMapper mapper, IRequisicaoService requisicaoService, IProdutoService produtoService, IEstoqueService estoqueService, ISetorService setorService)
        {
            _itemRequisicaoRepository = itemRequisicaoRepository;
            configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItensReq, ItemRequisicaoGetDTO>();
                cfg.CreateMap<ItemRequisicaoGetDTO,ItensReq>();
            });
            _mapper = configurationMapper.CreateMapper();
=======
        private readonly IRequisicaoService _requisicaoService;
        private readonly IProdutoService _produtoService;
        private readonly IEstoqueService _estoqueService;
        private readonly ISetorService _setorService;


        public ItemRequisicaoService(IItemRequisicaoRepository itemRequisicaoRepository, IRequisicaoService requisicaoService, IProdutoService produtoService, IEstoqueService estoqueService, ISetorService setorService)
        {
            _itemRequisicaoRepository = itemRequisicaoRepository;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            _requisicaoService = requisicaoService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _setorService = setorService;
<<<<<<< HEAD
        }

        public async Task<ItemRequisicaoGetDTO> Create(int id, ItemRequisicaoPostDTO itemRequisicaoView)
        {
            try
            {
                if (await ExisteRequisicao(id) && await VerificarRelacionamentosItemReq(id, itemRequisicaoView))
                {
                    decimal totalItem = itemRequisicaoView.QtdPro * itemRequisicaoView.PreUnit;
                    var itemRequisicao = CriarItemRequisicao(id, itemRequisicaoView, totalItem);

                    if (await VerificarEstoqueSuficiente(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.QtdPro))
                    {
                        var resultItem = await _itemRequisicaoRepository.Create(itemRequisicao);
                        if (resultItem != null)
                        {
                            await AtualizarEstoque(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicaoView.QtdPro);
                            await VerificarEstoqueMinimo(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.IdReq);
                            return _mapper.Map<ItemRequisicaoGetDTO>(resultItem);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Quantidade de produto em estoque insuficiente");
=======

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

                        if (await VerificarEstoqueSuficiente(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.QtdPro))
                        {
                            var resultItem = await _itemRequisicaoRepository.Create(itemRequisicao);
                            if (resultItem != null)
                            {
                                await AtualizarEstoque(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicaoView.QtdPro);
                                await VerificarEstoqueMinimo(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.IdReq);
                                return resultItem;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Quantidade de produto em estoque insuficiente");
                        }
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
<<<<<<< HEAD
        }

        public async Task<ItemRequisicaoGetDTO> Delete(int id)
=======

        }
        public async Task<ItensReq> Delete(int id)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

<<<<<<< HEAD
            return _mapper.Map<ItemRequisicaoGetDTO>(await _itemRequisicaoRepository.Delete(itemRequisicao));
        }

        public async Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll()
        {
            var items = await _itemRequisicaoRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemRequisicaoGetDTO>>(items);
        }

        public async Task<ItemRequisicaoGetDTO> GetById(int id)
        {
            var item = await _itemRequisicaoRepository.GetById(id);
            return _mapper.Map<ItemRequisicaoGetDTO>(item);
        }

        public async Task<ItemRequisicaoGetDTO> Update(int id, ItemRequisicaoPutDTO entity)
=======
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
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

<<<<<<< HEAD
            var updatedItem = _mapper.Map(entity, itemRequisicao);
            updatedItem.TotalItem = updatedItem.QtdPro * updatedItem.PreUnit;
            updatedItem.TotalReal = updatedItem.TotalItem;
            var result = await _itemRequisicaoRepository.Update(updatedItem);
            return _mapper.Map<ItemRequisicaoGetDTO>(result);
=======
            return await _itemRequisicaoRepository.Update(entity);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        }

        private async Task VerificarEstoqueMinimo(int idPro, int idSec, int idReq)
        {
            var produto = await _produtoService.GetById(idPro);
            var estoqueProduto = await _estoqueService.GetById(idPro, idSec);
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

<<<<<<< HEAD
        private async Task<bool> VerificarRelacionamentosItemReq(int Idreq, ItemRequisicaoPostDTO itemReq)
=======
        private async Task<bool> VerificarRelacionamentosItemReq(int Idreq, CreateItemRequisicaoViewModel itemReq)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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

<<<<<<< HEAD
        private ItensReq CriarItemRequisicao(int id, ItemRequisicaoPostDTO itemRequisicaoView, decimal totalItem)
=======
        private ItensReq CriarItemRequisicao(int id, CreateItemRequisicaoViewModel itemRequisicaoView, decimal totalItem)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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

<<<<<<< HEAD
        private async Task AtualizarEstoque(int IdPro, int IdSec, decimal quantidadeSaida)
=======
        private async Task AtualizarEstoque(int IdPro,int IdSec, decimal quantidadeSaida)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            await _estoqueService.RemoverEstoque(IdPro, IdSec, quantidadeSaida);
        }

        public async Task<bool> VerificarEstoqueSuficiente(int IdPro, int IdSec, decimal quantidadeSaida)
        {
            var produto = await _produtoService.GetById(IdPro);
            if (produto == null)
                return false;

            var estoque = await _estoqueService.GetById(produto.IdPro, IdSec);
            if (estoque == null)
                return false;

            return quantidadeSaida <= estoque.QtdPro;
        }
<<<<<<< HEAD
=======


>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
