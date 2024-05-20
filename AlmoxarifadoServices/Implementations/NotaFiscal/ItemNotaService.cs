using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
<<<<<<< HEAD
using AlmoxarifadoInfrastructure.Data.Repositories;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemNotaFiscal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoServices.Implementations
{
    public class ItemNotaService : IItemNotaService
    {
        private readonly IItemNotaRepository _repository;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IProdutoService _produtoService;
        private readonly ISecretariaService _secretariaService;
        private readonly IEstoqueService _estoqueService;
<<<<<<< HEAD
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration configurationMapper;


        public ItemNotaService(IItemNotaRepository repository, INotaFiscalService notaFiscalService, IProdutoService produtoService, ISecretariaService secretariaService, IEstoqueService estoqueService, INotaFiscalRepository notaFiscalRepository)
=======


        public ItemNotaService(IItemNotaRepository repository, INotaFiscalService notaFiscalService, IProdutoService produtoService, ISecretariaService secretariaService, IEstoqueService estoqueService)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            _repository = repository;
            _notaFiscalService = notaFiscalService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _secretariaService = secretariaService;
<<<<<<< HEAD
            _notaFiscalRepository = notaFiscalRepository;

            configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItensNotum, ItemNotaFiscalGetDTO>();
                cfg.CreateMap<ItemNotaFiscalGetDTO, ItensNotum>();
                cfg.CreateMap<NotaFiscalPutDTO, NotaFiscalGetDTO>();
                cfg.CreateMap<NotaFiscalGetDTO, NotaFiscalPutDTO>();
            });
            _mapper = configurationMapper.CreateMapper();
        }

        public async Task<ItemNotaFiscalGetDTO> Create(int idNotaFiscal, ItemNotaFiscalPostDTO itemFiscal)
=======
        }

        public async Task<ItensNotum> Create(int idNotaFiscal, CreateItemNotaFiscalViewModel itemFiscal)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
                        await AtualizarEstoque(itemFiscal.IdPro, itemFiscal.IdPro, itemFiscal.QtdPro);
                        await _notaFiscalService.AdicionarItem(notaFiscal);
<<<<<<< HEAD
                        return _mapper.Map<ItemNotaFiscalGetDTO>(resultItem);
=======
                        return item;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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


<<<<<<< HEAD
        public async Task<ItemNotaFiscalGetDTO> Delete(int id)
=======
        public async Task<ItensNotum> Delete(int id)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var item = await _repository.GetById(id);
            if (item != null)
            {
<<<<<<< HEAD
                var itemResult = await _repository.Delete(item);
                return _mapper.Map<ItemNotaFiscalGetDTO>(itemResult);
=======
                await _repository.Delete(item);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            return null;
        }

<<<<<<< HEAD
        public async Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll()
        {
           var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ItemNotaFiscalGetDTO>>(list);
        }

        public async Task<ItemNotaFiscalGetDTO> GetById(int id)
        {
            var item = await _repository.GetById(id);
            return _mapper.Map<ItemNotaFiscalGetDTO>(item);
        }

        public async Task<ItemNotaFiscalGetDTO> Update(int id, ItemNotaFiscalPutDTO entity)
=======
        public async Task<IEnumerable<ItensNotum>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ItensNotum> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ItensNotum> Update(int id, ItensNotum entity)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var ItemNota = await _repository.GetById(id);
            if (ItemNota == null)
                return null;
<<<<<<< HEAD
            ItemNota.PreUnit = entity.PreUnit;
            ItemNota.QtdPro = entity.QtdPro;
            ItemNota.TotalItem = entity.QtdPro * entity.PreUnit;
            var result = await _repository.Update(ItemNota);
            return _mapper.Map<ItemNotaFiscalGetDTO>(result);
        }

        private async Task<bool> VerificarRelacionamentosItem(ItemNotaFiscalPostDTO itemFiscal)
=======

            return await _repository.Update(entity);
        }

        private async Task<bool> VerificarRelacionamentosItem(CreateItemNotaFiscalViewModel itemFiscal)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
            var notaFiscal = await _notaFiscalRepository.GetById(id);
=======
            var notaFiscal = await _notaFiscalService.GetById(id);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
        private ItensNotum CriarItemNotaFiscal(ItemNotaFiscalPostDTO itemFiscal, int idNotaFiscal)
=======
        private ItensNotum CriarItemNotaFiscal(CreateItemNotaFiscalViewModel itemFiscal, int idNotaFiscal)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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



        private async Task AtualizarEstoque(int IdPro, int IdSec, decimal quantidadeSaida)
        {
            await _estoqueService.AdicionarEstoque(IdPro, IdSec, quantidadeSaida);
        }

    }
}
