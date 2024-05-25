using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoInfrastructure.Data.Repositories;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IEstoqueRepository repository)
        {
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

        public async Task<Estoque> Update(int id, Estoque entity)
        {
            var estoque = await _repository.GetById(id, entity.IdSec);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            return await _repository.Update(entity);
        }

        public async Task<Estoque> AdicionarEstoque(int id, int idSec, decimal quantidade)
        {
            var estoque = await _repository.GetById(id, idSec);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            estoque.AdicionarEstoque(quantidade);
            return await _repository.Update(estoque);
        }

        public async Task<Estoque> RemoverEstoque(int id, int idSec, decimal quantidade)
        {
            var estoque = await _repository.GetById(id, idSec);
            if(estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            estoque.RemoverEstoque(quantidade);
            return await _repository.Update(estoque);
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
