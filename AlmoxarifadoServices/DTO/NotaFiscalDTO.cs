using System.ComponentModel.DataAnnotations;

namespace AlmoxarifadoServices.DTO
{
    public class NotaFiscalComItensPostlDTO
    {
        [Required(ErrorMessage = "A nota fiscal é obrigatória.")]
        public NotaFiscalPostDTO NotaFiscal { get; set; } = null!;

        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "A nota fiscal deve ter pelo menos um item.")]
        public List<ItemNotaFiscalPostDTO> Itens { get; set; } = new List<ItemNotaFiscalPostDTO>();
    }

    public class NotaFiscalGetDTO
    {
        public int IdNota { get; set; }
        public int IdFor { get; set; }
        public int IdSec { get; set; }
        public string NumNota { get; set; } = null!;
        public int QtdItem { get; set; }
        public decimal ValorNota { get; set; }
        public int Ano { get; set; }
        public int? Mes { get; set; }
        public int IdTipoNota { get; set; }
    }

    public class NotaFiscalPutDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de itens deve ser pelo menos 1.")]
        public int QtdItem { get; set; }

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
        public string? ObservacaoNota { get; set; }
    }
    public class NotaFiscalPostDTO
    {
        [Required(ErrorMessage = "O Id do Fornecedor é obrigatório.")]
        public int IdFor { get; set; }

        [Required(ErrorMessage = "O Id da Seção é obrigatório.")]
        public int IdSec { get; set; }

        [Required(ErrorMessage = "O número da nota é obrigatório.")]
        [StringLength(50, ErrorMessage = "O número da nota deve ter no máximo 50 caracteres.")]
        public string NumNota { get; set; } = null!;

        [Required(ErrorMessage = "A quantidade de itens é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de itens deve ser pelo menos 1.")]
        public int QtdItem { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(2000, 2100, ErrorMessage = "O ano deve estar entre 2000 e 2100.")]
        public int Ano { get; set; }

        [Range(1, 12, ErrorMessage = "O mês deve estar entre 1 e 12.")]
        public int? Mes { get; set; }

        [Required(ErrorMessage = "O Id do Tipo de Nota é obrigatório.")]
        public int IdTipoNota { get; set; }

        [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
        public string? ObservacaoNota { get; set; }
    }

    public class NotaFiscalComItensGetDTO
    {
        public NotaFiscalGetDTO NotaFiscal { get; set; }
        public List<ItemNotaFiscalGetDTO> Itens { get; set; }
    }
}
