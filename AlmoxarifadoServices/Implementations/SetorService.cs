using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _repository;

        public SetorService(ISetorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Setor> ObterPorId(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
