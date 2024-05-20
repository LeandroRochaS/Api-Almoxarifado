
namespace AlmoxarifadoServices.DTO
{
    public class GrupoPostDTO
    {
        public string NOME_GRU { get; set; }
        public string? SUGESTAO_GRU { get; set; }
    }

    public class GrupoGetDTO
    {
        public int ID_GRU { get; set; }
        public string NOME_GRU { get; set; }
        public string? SUGESTAO_GRU { get; set; }
    }
}
