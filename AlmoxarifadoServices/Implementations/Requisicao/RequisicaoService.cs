<<<<<<< HEAD
﻿using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
=======
﻿ using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.Requisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
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
<<<<<<< HEAD
        private readonly IMapper _mapper;
=======


>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

        public RequisicaoService(IRequisicaoRepository requisicaoRepository, IClienteService clienteService, ISetorService setorService, ISecretariaService secretariaService)
        {
            _requisicaoRepository = requisicaoRepository;
            _clienteService = clienteService;
            _setorService = setorService;
            _secretariaService = secretariaService;
<<<<<<< HEAD

            var configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Requisicao, RequisicaoGetDTO>();
                cfg.CreateMap<RequisicaoPostDTO, Requisicao>();
            });

            _mapper = configurationMapper.CreateMapper();
        }

        public async Task<RequisicaoGetDTO> Create(RequisicaoPostDTO requisicaoView)
=======
        }

        public async Task<Requisicao> Create(CreateRequisicaoViewModel requisicaoView)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            try
            {
                if (await VerificarRelacionamentosRequisicao(requisicaoView))
                {
<<<<<<< HEAD
                    var requisicao = _mapper.Map<Requisicao>(requisicaoView);
                    requisicao.DataReq = DateTime.Now;
                    requisicao.TotalReq = 0;
                    requisicao.QtdIten = 0;

                    var result = await _requisicaoRepository.Create(requisicao);
                    return _mapper.Map<RequisicaoGetDTO>(result);
=======
                    Requisicao requisicao = CriarRequisicao(requisicaoView);
                    return await _requisicaoRepository.Create(requisicao);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
<<<<<<< HEAD
            return null;
        }

        public async Task<RequisicaoGetDTO> Delete(int id)
=======

            return null;
        }

        public async Task<Requisicao> Delete(int id)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

<<<<<<< HEAD
            var deletedRequisicao = await _requisicaoRepository.Delete(requisicao);
            return _mapper.Map<RequisicaoGetDTO>(deletedRequisicao);
        }

        public async Task<IEnumerable<RequisicaoGetDTO>> GetAll()
        {
            var requisicoes = await _requisicaoRepository.GetAll();
            return _mapper.Map<IEnumerable<RequisicaoGetDTO>>(requisicoes);
        }

        public async Task<RequisicaoGetDTO> GetById(int id)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            return _mapper.Map<RequisicaoGetDTO>(requisicao);
        }

        public async Task<RequisicaoGetDTO> Update(int id, RequisicaoPostDTO requisicaoView)
=======
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
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

<<<<<<< HEAD
            _mapper.Map(requisicaoView, requisicao);
            var updatedRequisicao = await _requisicaoRepository.Update(requisicao);
            return _mapper.Map<RequisicaoGetDTO>(updatedRequisicao);
        }

        private async Task<bool> VerificarRelacionamentosRequisicao(RequisicaoPostDTO requisicao)
=======
            requisicao.QtdIten = entity.QtdIten;
            requisicao.TotalReq = entity.TotalReq;


            return await _requisicaoRepository.Update(requisicao);
        }

        private async Task<bool> VerificarRelacionamentosRequisicao(CreateRequisicaoViewModel requisicao)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            return requisicao.IdCli != 0 &&
                   requisicao.IdSec != 0 &&
                   requisicao.IdSet != 0 &&
                   await _clienteService.GetById(requisicao.IdCli) != null &&
                   await _setorService.GetById(requisicao.IdSet) != null &&
                   await _secretariaService.GetById(requisicao.IdSec) != null;
        }
<<<<<<< HEAD
=======


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

>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
