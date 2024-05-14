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
    public class SecretariaService : ISecretariaService
    {
        private readonly ISecretariaRepository _repository;

        public SecretariaService(ISecretariaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Secretarium> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
