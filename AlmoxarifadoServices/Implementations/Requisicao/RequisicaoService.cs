using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;

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
            try
            {
                if (await VerificarRelacionamentosRequisicao(requisicaoView))
                {
                    var requisicao = _mapper.Map<Requisicao>(requisicaoView);
                    requisicao.DataReq = DateTime.Now;
                    requisicao.TotalReq = 0;
                    requisicao.QtdIten = 0;

                    var result = await _requisicaoRepository.Create(requisicao);
                    return _mapper.Map<RequisicaoGetDTO>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
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

        public async Task<RequisicaoGetDTO> Update(int id, RequisicaoPostDTO requisicaoView)
        {
            var requisicao = await _requisicaoRepository.GetById(id);
            if (requisicao == null)
            {
                throw new ArgumentException("Requisição não encontrada.");
            }

            _mapper.Map(requisicaoView, requisicao);
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
