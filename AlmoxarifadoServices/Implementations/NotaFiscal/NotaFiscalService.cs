using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.NotaFiscal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;
        private readonly IFornecedorService _fornecedorService;
        private readonly ISecretariaService _secretariaService;

        public NotaFiscalService(INotaFiscalRepository repository, IFornecedorService fornecedorService, ISecretariaService secretariaService)
        {
            _repository = repository;
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
        }

        public async Task<NotaFiscal> Create(CreateNotaFiscalViewModel notaFiscalView)
        {
            try
            {
                await VerificarRelacionamentosNotaFiscal(notaFiscalView);

                var notaFiscal = CriarNotaFiscal(notaFiscalView);

                return await _repository.Create(notaFiscal);
            }
            catch (Exception ex)
            {
                // Logar a exceção ou tratar conforme necessário
                throw ex;
            }
        }
        public async Task<NotaFiscal> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<NotaFiscal> AdicionarItem(NotaFiscal notaFiscal)
        {
            var notaFiscalExistente = await _repository.GetById(notaFiscal.IdNota);
            if (notaFiscalExistente == null)
            {
                throw new ArgumentException("Nota fiscal não encontrada.");
            }
            notaFiscal.QtdItem += 1;
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

        private NotaFiscal CriarNotaFiscal(CreateNotaFiscalViewModel notaFiscalView)
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
