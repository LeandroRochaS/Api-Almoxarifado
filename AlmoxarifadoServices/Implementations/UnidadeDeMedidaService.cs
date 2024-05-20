using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class UnidadeDeMedidaService : IUnidadeDeMedidaService
    {
        private readonly IUnidadeDeMedidaRepository _repository;

        public UnidadeDeMedidaService(IUnidadeDeMedidaRepository repository)
        {
            _repository = repository;
        }

        public async Task<UnidadeMedidum> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
