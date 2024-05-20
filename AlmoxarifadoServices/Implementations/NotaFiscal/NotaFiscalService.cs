using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;

namespace AlmoxarifadoServices.Implementations
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _repository;
        private readonly IFornecedorService _fornecedorService;
        private readonly ISecretariaService _secretariaService;
        private readonly IMapper _mapper;

        public NotaFiscalService(INotaFiscalRepository repository, IFornecedorService fornecedorService, ISecretariaService secretariaService, IMapper mapper)
        {
            _repository = repository;
            _fornecedorService = fornecedorService;
            _secretariaService = secretariaService;
            _mapper = mapper;
        }

        public async Task<NotaFiscal> Create(NotaFiscalPostDTO notaFiscalView)
        {
            try
            {
                await VerificarRelacionamentosNotaFiscal(notaFiscalView);

                var notaFiscalT = CriarNotaFiscal(notaFiscalView);

                var notaFiscalResult = await _repository.Create(notaFiscalT);
                return notaFiscalResult;
            }
            catch (Exception ex)
            {
                // Logar a exceção ou tratar conforme necessário
                throw ex;
            }
        }
        public async Task<NotaFiscalGetDTO> GetById(int id)
        {
            var nota = await _repository.GetById(id);
            return _mapper.Map<NotaFiscalGetDTO>(nota);
        }

        public async Task<NotaFiscalGetDTO> AdicionarItem(NotaFiscal notaFiscal)
        {
            var notaFiscalExistente = await _repository.GetById(notaFiscal.IdNota);
            if (notaFiscalExistente == null)
            {
                throw new ArgumentException("Nota fiscal não encontrada.");
            }
            notaFiscal.QtdItem += 1;
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

        private NotaFiscal CriarNotaFiscal(NotaFiscalPostDTO notaFiscalView)
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
