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
