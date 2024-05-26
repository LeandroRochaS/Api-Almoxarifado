using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class RemoverEstoqueStrategy : IAtualizarEstoqueStrategy
    {
        private readonly IEstoqueRepository _repository;

        public RemoverEstoqueStrategy(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Estoque> AtualizarEstoque(Estoque estoque, decimal quantidade)
        {
            estoque.RemoverEstoque(quantidade);
            return await _repository.Update(estoque);
        }
    }
}
