using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class ClasseService : IClasseService
    {
        private readonly IClasseRepository _repository;

        public ClasseService(IClasseRepository repository)
        {
            _repository = repository;
        }
        public async Task<Classe> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
