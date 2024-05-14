using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemNotaService : IItemNotaService
    {
        private readonly IItemNotaRepository _repository;

        public ItemNotaService(IItemNotaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ItensNotum> Create(ItensNotum entity)
        {
            return await _repository.Create(entity); 
        }

        public async Task<ItensNotum> Delete(int id)
        {
            var item = await _repository.GetById(id);
            if(item != null)
            {
                await _repository.Delete(item);
            }
            return null;
        }

        public async Task<IEnumerable<ItensNotum>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ItensNotum> GetById(int id)
        {
          return await _repository.GetById(id);
        }

        public async Task<ItensNotum> Update(int id, ItensNotum entity)
        {
           return await _repository.Update(entity);
        }
    }
}
