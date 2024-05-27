using System.ComponentModel.DataAnnotations;

namespace AlmoxarifadoServices.DTO
{
    public class RequisicaoPostDTO
    {
        [Required(ErrorMessage = "O Id do Cliente é obrigatório.")]
        public int IdCli { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(2000, 2100, ErrorMessage = "O ano deve estar entre 2000 e 2100.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O mês é obrigatório.")]
        [Range(1, 12, ErrorMessage = "O mês deve estar entre 1 e 12.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "O Id da Seção é obrigatório.")]
        public int IdSec { get; set; }

        [Required(ErrorMessage = "O Id do Setor é obrigatório.")]
        public int IdSet { get; set; }

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
        public string? Observacao { get; set; }
    }

    public class RequisicaoPutDTO
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "O total da requisição deve ser positivo.")]
        public decimal? TotalReq { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de itens deve ser pelo menos 1.")]
        public int? QtdIten { get; set; }

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
        public string? Observacao { get; set; }
    }

    public class RequisicaoGetDTO
    {
        public int IdReq { get; set; }
        public int IdSec { get; set; }
        public int IdSet { get; set; }
        public int IdCli { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataReq { get; set; }
        public decimal TotalReq { get; set; }
        public int QtdIten { get; set; }
    }

    public class RequisicaoComItensPostDTO
    {
        [Required(ErrorMessage = "A requisição é obrigatória.")]
        public RequisicaoPostDTO Requisicao { get; set; } = null!;

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "A requisição deve ter pelo menos um item.")]
        public List<ItemRequisicaoPostDTO> Itens { get; set; } = new List<ItemRequisicaoPostDTO>();
    }

    public class RequisicaoComItensGetDTO
    {
        public RequisicaoGetDTO Requisicao { get; set; }
        public List<ItemRequisicaoGetDTO> Itens { get; set; }
    }
}
