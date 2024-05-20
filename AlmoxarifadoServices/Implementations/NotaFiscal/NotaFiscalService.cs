using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
<<<<<<< HEAD
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
=======
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

namespace AlmoxarifadoServices.Implementations
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;
        private readonly IFornecedorService _fornecedorService;
        private readonly ISecretariaService _secretariaService;
<<<<<<< HEAD
        private readonly IMapper _mapper;

        public NotaFiscalService(INotaFiscalRepository repository, IFornecedorService fornecedorService, ISecretariaService secretariaService, IMapper mapper)
=======

        public NotaFiscalService(INotaFiscalRepository repository, IFornecedorService fornecedorService, ISecretariaService secretariaService)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            _repository = repository;
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
<<<<<<< HEAD
            _mapper = mapper;
        }

        public async Task<NotaFiscal> Create(NotaFiscalPostDTO notaFiscalView)
=======
        }

        public async Task<NotaFiscal> Create(CreateNotaFiscalViewModel notaFiscalView)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            try
            {
                await VerificarRelacionamentosNotaFiscal(notaFiscalView);

<<<<<<< HEAD
                var notaFiscalT = CriarNotaFiscal(notaFiscalView);

                var notaFiscalResult = await _repository.Create(notaFiscalT);
                return notaFiscalResult;
=======
                var notaFiscal = CriarNotaFiscal(notaFiscalView);

                return await _repository.Create(notaFiscal);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
            }
            catch (Exception ex)
            {
                // Logar a exceção ou tratar conforme necessário
                throw ex;
            }
        }
<<<<<<< HEAD
        public async Task<NotaFiscalGetDTO> GetById(int id)
        {
            var nota = await _repository.GetById(id);
            return _mapper.Map<NotaFiscalGetDTO>(nota);
        }

        public async Task<NotaFiscalGetDTO> AdicionarItem(NotaFiscal notaFiscal)
=======
        public async Task<NotaFiscal> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<NotaFiscal> AdicionarItem(NotaFiscal notaFiscal)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            var notaFiscalExistente = await _repository.GetById(notaFiscal.IdNota);
            if (notaFiscalExistente == null)
            {
                throw new ArgumentException("Nota fiscal não encontrada.");
            }
            notaFiscal.QtdItem += 1;
<<<<<<< HEAD
            var notaResult = await _repository.Update(notaFiscal);
            return _mapper.Map<NotaFiscalGetDTO>(notaResult);
        }

        public async Task<IEnumerable<NotaFiscalGetDTO>> GetAll()
        {
            var list = await _repository.GetAll();
            return _mapper.Map<IEnumerable<NotaFiscalGetDTO>>(list);
        }

        public async Task<NotaFiscalGetDTO> Update(int id, NotaFiscalPutDTO notaFiscal)
        {
            var nota = await _repository.GetById(id);
            if (nota == null)
                return null;
            nota.QtdItem = notaFiscal.QtdItem;          
            var result = await _repository.Update(nota);
            return _mapper.Map<NotaFiscalGetDTO>(result);
        }

        public async Task<NotaFiscalGetDTO> Delete(int id)
        {
            var nota = await _repository.GetById(id);   
            if (nota == null)
                return null;
            var result = await _repository.Delete(nota);
            return _mapper.Map<NotaFiscalGetDTO>(result);
        }

        private async Task<bool> VerificarRelacionamentosNotaFiscal(NotaFiscalPostDTO notaFiscal)
=======
            return await _repository.Update(notaFiscal);
        }

        public async Task<IEnumerable<NotaFiscal>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<NotaFiscal> Update(int id, NotaFiscal notaFiscal)
        {
            var nota = await GetById(id);
            if (nota == null)
                return null;
            return await _repository.Update(notaFiscal);
        }

        public async Task<NotaFiscal> Delete(int id)
        {
            var nota = await GetById(id);   
            if (nota == null)
                return null;
            return await _repository.Delete(nota);
        }

        private async Task<bool> VerificarRelacionamentosNotaFiscal(CreateNotaFiscalViewModel notaFiscal)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            if (notaFiscal.IdFor == 0 || notaFiscal.IdSec == 0)
                throw new ArgumentException("Fornecedor ou Secretaria inválida");

            var fornecedor = await ObterFornecedorPorId(notaFiscal.IdFor);
            if (fornecedor == null)
                throw new ArgumentException("Fornecedor não encontrado");

            var secretaria = await ObterSecretariaPorId(notaFiscal.IdSec);
            if (secretaria == null)
                throw new ArgumentException("Secretaria não encontrada");

            return true;
        }

<<<<<<< HEAD
        private NotaFiscal CriarNotaFiscal(NotaFiscalPostDTO notaFiscalView)
=======
        private NotaFiscal CriarNotaFiscal(CreateNotaFiscalViewModel notaFiscalView)
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
        {
            return new NotaFiscal
            {
                Ano = notaFiscalView.Ano,
                DataNota = DateTime.Now,
                EmpenhoNum = 0,
                Icms = 0,
                IdFor = notaFiscalView.IdFor,
                IdSec = notaFiscalView.IdSec,
                Iss = 0,
                Mes = notaFiscalView.Mes,
                IdTipoNota = notaFiscalView.IdTipoNota,
                ObservacaoNota = notaFiscalView.ObservacaoNota,
                QtdItem = notaFiscalView.QtdItem,
                NumNota = notaFiscalView.NumNota,
                ValorNota = 0
            };
        }

        private async Task<Fornecedor> ObterFornecedorPorId(int id)
        {
            return await _fornecedorService.GetById(id);
        }

        private async Task<Secretarium> ObterSecretariaPorId(int id)
        {
            return await _secretariaService.GetById(id);
        }
    }
}
