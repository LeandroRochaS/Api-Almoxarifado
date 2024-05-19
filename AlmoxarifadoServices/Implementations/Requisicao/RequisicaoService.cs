 using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.Requisicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class RequisicaoService : IRequisicaoService
    {
        private readonly IRequisicaoRepository _requisicaoRepository;
        private readonly IClienteService _clienteService;
        private readonly ISetorService _setorService;
        private readonly ISecretariaService _secretariaService;



        public RequisicaoService(IRequisicaoRepository requisicaoRepository, IClienteService clienteService, ISetorService setorService, ISecretariaService secretariaService)
        {
            _requisicaoRepository = requisicaoRepository;
            _clienteService = clienteService;
            _setorService = setorService;
            _secretariaService = secretariaService;
        }

        public async Task<Requisicao> Create(CreateRequisicaoViewModel requisicaoView)
        {
            try
            {
                if (await VerificarRelacionamentosRequisicao(requisicaoView))
                {
                    Requisicao requisicao = CriarRequisicao(requisicaoView);
                    return await _requisicaoRepository.Create(requisicao);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
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

            requisicao.QtdIten = entity.QtdIten;
            requisicao.TotalReq = entity.TotalReq;


            return await _requisicaoRepository.Update(requisicao);
        }

        private async Task<bool> VerificarRelacionamentosRequisicao(CreateRequisicaoViewModel requisicao)
        {
            return requisicao.IdCli != 0 &&
                   requisicao.IdSec != 0 &&
                   requisicao.IdSet != 0 &&
                   await _clienteService.GetById(requisicao.IdCli) != null &&
                   await _setorService.GetById(requisicao.IdSet) != null &&
                   await _secretariaService.GetById(requisicao.IdSec) != null;
        }


        private Requisicao CriarRequisicao(CreateRequisicaoViewModel requisicaoView)
        {
            return new Requisicao
            {
                Ano = requisicaoView.Ano,
                DataReq = DateTime.Now,
                IdCli = requisicaoView.IdCli,
                IdSec = requisicaoView.IdSec,
                IdSet = requisicaoView.IdSet,
                Mes = requisicaoView.Mes,
                Observacao = requisicaoView.Observacao,
                TotalReq = 0,
                QtdIten = 0
            };
        }

    }
}
