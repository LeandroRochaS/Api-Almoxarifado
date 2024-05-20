using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Fornecedor> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
