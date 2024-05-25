using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoInfrastructure.Data.Repositories;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemNotaService : IItemNotaService
    {
        private readonly IItemNotaRepository _repository;
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IProdutoService _produtoService;
        private readonly ISecretariaService _secretariaService;
        private readonly IEstoqueService _estoqueService;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration configurationMapper;

        public ItemNotaService(
            IItemNotaRepository repository,
            INotaFiscalService notaFiscalService,
            IProdutoService produtoService,
            ISecretariaService secretariaService,
            IEstoqueService estoqueService,
            INotaFiscalRepository notaFiscalRepository
        )
        {
            _repository = repository;
            _notaFiscalService = notaFiscalService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
            _secretariaService = secretariaService;
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

        public async Task<ItemNotaFiscalGetDTO> Create(
            int idNotaFiscal,
            ItemNotaFiscalPostDTO itemFiscal
        )
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
                        await AtualizarEstoque(
                            itemFiscal.IdPro,
                            itemFiscal.IdSec,
                            itemFiscal.QtdPro
                        );
                        await _notaFiscalService.AdicionarItem(notaFiscal);
                        return _mapper.Map<ItemNotaFiscalGetDTO>(resultItem);
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

        public async Task<ItemNotaFiscalGetDTO> Delete(KeyItemNotaDTO keys)
        {
            var item = await _repository.GetById(keys.NumItem, keys.IdProduto, keys.IdNota, keys.IdSecretaria);
            if (item != null)
            {
                var itemResult = await _repository.Delete(item);
                return _mapper.Map<ItemNotaFiscalGetDTO>(itemResult);
            }
            return null;
        }

        public async Task<IEnumerable<ItemNotaFiscalGetDTO>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<ItemNotaFiscalGetDTO>>(list);
        }

        public async Task<ItemNotaFiscalGetDTO> GetById(KeyItemNotaDTO keys)
        {
            var item = await _repository.GetById(keys.NumItem, keys.IdProduto, keys.IdNota, keys.IdSecretaria);
            return _mapper.Map<ItemNotaFiscalGetDTO>(item);
        }

        public async Task<ItemNotaFiscalGetDTO> Update(KeyItemNotaDTO keys, ItemNotaFiscalPutDTO entity)
        {
            var ItemNota = await _repository.GetById(keys.NumItem, keys.IdProduto, keys.IdNota, keys.IdSecretaria);
            if (ItemNota == null)
                return null;
            ItemNota.PreUnit = entity.PreUnit;
            ItemNota.QtdPro = entity.QtdPro;
            ItemNota.TotalItem = entity.QtdPro * entity.PreUnit;
            var result = await _repository.Update(ItemNota);
            if(result == 1)
                return _mapper.Map<ItemNotaFiscalGetDTO>(ItemNota);
            return null;
        }

        private async Task<bool> VerificarRelacionamentosItem(ItemNotaFiscalPostDTO itemFiscal)
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
            var notaFiscal = await _notaFiscalRepository.GetById(id);
            if (notaFiscal == null)
                throw new ArgumentException("Nota Fiscal não encontrada");

            return notaFiscal;
        }

        private async Task<Secretarium> ObterSecretariaPorId(int id)
        {
            return await _secretariaService.GetById(id);
        }

        private async Task<ProdutoGetDTO> ObterProdutoPorId(int id)
        {
            return await _produtoService.GetById(id);
        }

        private ItensNotum CriarItemNotaFiscal(ItemNotaFiscalPostDTO itemFiscal, int idNotaFiscal)
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
