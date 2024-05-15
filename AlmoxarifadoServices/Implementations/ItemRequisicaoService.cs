using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class ItemRequisicaoService : IItemRequisicaoService
    {
        private readonly IItemRequisicaoRepository _itemRequisicaoRepository;

        public ItemRequisicaoService(IItemRequisicaoRepository itemRequisicaoRepository)
        {
            _itemRequisicaoRepository = itemRequisicaoRepository;
        }

        public async Task<ItensReq> Create(ItensReq entity)
        {
            return await _itemRequisicaoRepository.Create(entity);
        }

        public async Task<ItensReq> Delete(int id)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

            return await _itemRequisicaoRepository.Delete(itemRequisicao);
        }

        public async Task<IEnumerable<ItensReq>> GetAll()
        {
            return await _itemRequisicaoRepository.GetAll();
        }

        public async Task<ItensReq> GetById(int id)
        {
            return await _itemRequisicaoRepository.GetById(id);
        }

        public async Task<ItensReq> Update(int id, ItensReq entity)
        {
            var itemRequisicao = await _itemRequisicaoRepository.GetById(id);
            if (itemRequisicao == null)
            {
                throw new ArgumentException("Item de requisição não encontrado.");
            }

            return await _itemRequisicaoRepository.Update(entity);
        }
    }
}
