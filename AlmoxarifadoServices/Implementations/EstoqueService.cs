using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoInfrastructure.Data.Repositories;
using AlmoxarifadoServices.Implementations.EstoqueStrategy;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IEstoqueRepository repository, IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _repository = repository;
        }

        public async Task<Estoque> Create(Estoque entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<Estoque> Delete(int id, int idSec)
        {
            var estoque = await _repository.GetById(id, idSec);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            return await _repository.Delete(estoque);
        }

        public async Task<IEnumerable<Estoque>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Estoque> GetById(int id, int idSec)
        {
            return await _repository.GetById(id, idSec);
        }

        public async Task<Estoque> Update(Estoque entity)
        {
            var estoque = await _repository.GetById(entity.IdPro, entity.IdSec);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            return await _repository.Update(entity);
        }

        public async Task AtualizarEstoque(int id, int idSec, decimal quantidade, bool adicionar)
        {
            var estoque = await _repository.GetById(id, idSec);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            IAtualizarEstoqueStrategy strategy;
            if (adicionar)
            {
                strategy = new AdicionarEstoqueStrategy(_repository);
            }
            else
            {
                strategy = new RemoverEstoqueStrategy(_repository);
            }

            var contexto = new AtualizarEstoqueContext(strategy);
            await contexto.AtualizarEstoque(estoque, quantidade);
        }


        public async Task<bool> VerificarEstoqueSuficiente(
         int IdPro,
         int IdSec,
        decimal quantidadeSaida
     )
        {
            var produto = await _produtoRepository.GetById(IdPro);
            if (produto == null)
                return false;

            var estoque = await GetById(IdPro, IdSec);
            if (estoque == null)
                return false;

            return estoque.VerificarEstoqueSuficiente(quantidadeSaida);
        }
    }
}
