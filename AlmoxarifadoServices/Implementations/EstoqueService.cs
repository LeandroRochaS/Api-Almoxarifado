using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;

        public EstoqueService(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<Estoque> Create(Estoque entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<Estoque> Delete(int id)
        {
            var estoque = await _repository.GetById(id);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            return await _repository.Delete(estoque);
        }

        public async Task<IEnumerable<Estoque>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Estoque> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Estoque> Update(int id, Estoque entity)
        {
            var estoque = await _repository.GetById(id);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            return await _repository.Update(entity);
        }

        public async Task<Estoque> AdicionarEstoque(int id, decimal quantidade)
        {
            var estoque = await _repository.GetById(id);
            if (estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            estoque.QtdPro += quantidade;
            return await _repository.Update(estoque);
        }

        public async Task<Estoque> RemoverEstoque(int id, decimal quantidade)
        {
            var estoque = await _repository.GetById(id);
            if(estoque == null)
            {
                throw new ArgumentException("Registro de estoque não encontrado.");
            }

            estoque.QtdPro -= quantidade;
            return await _repository.Update(estoque);
        }
    }
}
