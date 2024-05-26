using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;
using NuGet.Protocol.Core.Types;
using System.Transactions;

namespace AlmoxarifadoServices.Implementations
{
    public class RequisicaoService : IRequisicaoService
    {
        private readonly IRequisicaoRepository _requisicaoRepository;
        private readonly IClienteService _clienteService;
        private readonly ISetorService _setorService;
        private readonly ISecretariaService _secretariaService;
        private readonly IMapper _mapper;

        public RequisicaoService(
            IRequisicaoRepository requisicaoRepository,
            IClienteService clienteService,
            ISetorService setorService,
            ISecretariaService secretariaService
        )
        {
            _requisicaoRepository = requisicaoRepository;
            _clienteService = clienteService;
            _setorService = setorService;
            _secretariaService = secretariaService;

            var configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Requisicao, RequisicaoGetDTO>();
                cfg.CreateMap<RequisicaoPostDTO, Requisicao>();
            });

            _mapper = configurationMapper.CreateMapper();
        }

        public async Task<RequisicaoGetDTO> Create(RequisicaoPostDTO requisicaoView)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (await VerificarRelacionamentosRequisicao(requisicaoView))
                    {
                        var requisicao = _mapper.Map<Requisicao>(requisicaoView);
                        requisicao.DataReq = DateTime.Now;
                        requisicao.TotalReq = 0;
                        requisicao.QtdIten = 0;

                        var result = await _requisicaoRepository.Create(requisicao);
                        scope.Complete();
                        return _mapper.Map<RequisicaoGetDTO>(result);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error ao tentar criar a requisição, verifique os dados.");
                }
                return null;
            }
        }
        public async Task<RequisicaoGetDTO> AtualizarTotalReq(int idReq)
        {
            var reqCItems = await _requisicaoRepository.GetByIdWithItens(idReq);
            if (reqCItems == null)
                throw new ArgumentException("Requisição não encontrada.");

            decimal total = 0.0m;
            foreach (ItensReq item in reqCItems.ItensReqs)
            {
                total += (decimal)item.TotalItem;
            }

            reqCItems.TotalReq = total;
            return _mapper.Map<RequisicaoGetDTO>(await _requisicaoRepository.Update(reqCItems));
        }

        public async Task<RequisicaoGetDTO> AdicionarItem(int idReq)
        {
            var reqFiscalExistente = await _requisicaoRepository.GetById(idReq);
            if (reqFiscalExistente == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }
            reqFiscalExistente.QtdIten += 1;
            var reqResult = await _requisicaoRepository.Update(reqFiscalExistente);
            return _mapper.Map<RequisicaoGetDTO>(reqResult);
        }

        public async Task<RequisicaoGetDTO> RemoverItem(int idReq)
        {
            var reqFiscalExistente = await _requisicaoRepository.GetById(idReq);
            if (reqFiscalExistente == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }
            reqFiscalExistente.QtdIten -= 1;
            var notaResult = await _requisicaoRepository.Update(reqFiscalExistente);
            return _mapper.Map<RequisicaoGetDTO>(notaResult);
        }

        public async Task<RequisicaoGetDTO> Delete(int id)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

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

        public async Task<RequisicaoGetDTO> Update(int id, RequisicaoPutDTO requisicaoDto)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

            requisicao.TotalReq = requisicaoDto.TotalReq;
            requisicao.QtdIten = requisicaoDto.QtdIten;
            requisicao.Observacao = requisicaoDto.Observacao;

            var updatedRequisicao = await _requisicaoRepository.Update(requisicao);
            return _mapper.Map<RequisicaoGetDTO>(updatedRequisicao);
        }

        private async Task<bool> VerificarRelacionamentosRequisicao(RequisicaoPostDTO requisicao)
        {
            return requisicao.IdCli != 0
                && requisicao.IdSec != 0
                && requisicao.IdSet != 0
                && await _clienteService.GetById(requisicao.IdCli) != null
                && await _setorService.GetById(requisicao.IdSet) != null
                && await _secretariaService.GetById(requisicao.IdSec) != null;
        }
    }
}
