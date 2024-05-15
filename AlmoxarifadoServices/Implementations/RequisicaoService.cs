using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class RequisicaoService : IRequisicaoService
    {
        private readonly IRequisicaoRepository _requisicaoRepository;

        public RequisicaoService(IRequisicaoRepository requisicaoRepository)
        {
            _requisicaoRepository = requisicaoRepository;
        }

        public async Task<Requisicao> Create(Requisicao entity)
        {
            return await _requisicaoRepository.Create(entity);
        }

        public async Task<Requisicao> Delete(int id)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

            return await _requisicaoRepository.Delete(requisicao);
        }

        public async Task<IEnumerable<Requisicao>> GetAll()
        {
            return await _requisicaoRepository.GetAll();
        }

        public async Task<Requisicao> GetById(int id)
        {
            return await _requisicaoRepository.GetById(id);
        }

        public async Task<Requisicao> Update(int id, Requisicao entity)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

           return await _requisicaoRepository.Update(entity);
        }
    }
}
