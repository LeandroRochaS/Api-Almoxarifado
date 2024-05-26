using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class AdicionarEstoqueStrategy : IAtualizarEstoqueStrategy
    {
        private readonly IEstoqueRepository _repository;

        public AdicionarEstoqueStrategy(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Estoque> AtualizarEstoque(Estoque estoque, decimal quantidade)
        {
            estoque.AdicionarEstoque(quantidade);
            return await _repository.Update(estoque);
        }
    }

}
