using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.DTO;
using AlmoxarifadoServices.Interfaces;
using AutoMapper;


namespace AlmoxarifadoServices.Implementations
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly MapperConfiguration configurationMapper;

        public GrupoService(IGrupoRepository pGrupoRepository)
        {
            _grupoRepository = pGrupoRepository;
            configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Grupo, GrupoGetDTO>();
                cfg.CreateMap<GrupoGetDTO, Grupo>();
            });
        }

        public List<GrupoGetDTO> ObterTodosGrupos()
        {
            var mapper = configurationMapper.CreateMapper();


            return mapper.Map<List<GrupoGetDTO>>(_grupoRepository.ObterTodosGrupos());
        }

        public Grupo ObterGrupoPorID(int id)
        {


            return _grupoRepository.ObterGrupoPorId(id);
        }

        public GrupoGetDTO CriarGrupo(GrupoPostDTO grupo)
        {
            var grupoSalvo = _grupoRepository.CriarGrupo(
                 new Grupo { NomeGru = grupo.NOME_GRU, SugestaoGru = grupo.SUGESTAO_GRU }
              );

            return new GrupoGetDTO
            {
                ID_GRU = grupoSalvo.IdGru,
                NOME_GRU = grupoSalvo.NomeGru,
                SUGESTAO_GRU = grupoSalvo.SugestaoGru
            };
        }

    }
}
