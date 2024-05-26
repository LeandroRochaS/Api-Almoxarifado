using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;

namespace AlmoxarifadoServices.Implementations
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _repository;
        private readonly IMapper mapper;
        private readonly MapperConfiguration configurationMapper;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
            configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdutoPostDTO, Produto>();
                cfg.CreateMap<Produto, ProdutoPutDTO>();
                cfg.CreateMap<ProdutoGetDTO, Produto>();
                cfg.CreateMap<Produto, ProdutoGetDTO>();
            });
            mapper = configurationMapper.CreateMapper();
        }

        public async Task<ProdutoGetDTO> Create(ProdutoPostDTO entity)
        {
            if (entity != null)
            {
                Produto produto = new Produto
                {
                    IdClas = entity.IdClasse,
                    IdUnMed = entity.IdUnMedida,
                    Descricao = entity.Descricao,
                    EstoqueMin = entity.EstoqueMin,
                    Observacao = entity.Observacao,
                    Perecivel = entity.Perecivel == AlmoxarifadoDomain.Enums.ProdutoPerecivelEnum.Sim ? true : false,
                    QtdEmbalagem = entity.QtdEmbalagem
                };


                var result = await _repository.Create(produto);
                return mapper.Map<ProdutoGetDTO>(result);
            }
            return null;
        }

        public async Task<ProdutoGetDTO> Delete(int id)
        {
            var produtoDb = await _repository.GetById(id);
            if (produtoDb != null)
            {
                var result = await _repository.Delete(produtoDb);
                return mapper.Map<ProdutoGetDTO>(result);
            }
            return null;
        }

        public async Task<IEnumerable<ProdutoGetDTO>> GetAll()
        {
            return mapper.Map<List<ProdutoGetDTO>>(await _repository.GetAll());
        }

        public async Task<ProdutoGetDTO> GetById(int id)
        {
            return mapper.Map<ProdutoGetDTO>(await _repository.GetById(id));
        }

        public async Task<ProdutoGetDTO> Update(int id, ProdutoPutDTO entity)
        {
            var produtoDb = await _repository.GetById(id);
            if (produtoDb != null)
            {
                produtoDb.EstoqueMin = entity.EstoqueMin;
                produtoDb.Observacao = entity.Observacao;
                produtoDb.QtdEmbalagem = entity.QtdEmbalagem;

                return mapper.Map<ProdutoGetDTO>(await _repository.Update(produtoDb));

            }
            return null;
        }
    }
}
