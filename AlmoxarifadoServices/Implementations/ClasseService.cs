using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
