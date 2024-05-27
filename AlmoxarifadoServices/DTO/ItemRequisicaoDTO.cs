using System.ComponentModel.DataAnnotations;

namespace AlmoxarifadoServices.DTO
{
    public class ItemRequisicaoPostDTO
    {
        [Required(ErrorMessage = "O número do item é obrigatório.")]
        public int NumItem { get; set; }

        [Required(ErrorMessage = "O Id do Produto é obrigatório.")]
        public int IdPro { get; set; }

        [Required(ErrorMessage = "O Id da Seção é obrigatório.")]
        public int IdSec { get; set; }

        [Required(ErrorMessage = "A quantidade do produto é obrigatória.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo.")]
        public decimal QtdPro { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser um valor positivo.")]
        public decimal PreUnit { get; set; }
    }

    public class ItemRequisicaoGetDTO
    {
        public int NumItem { get; set; }
        public int IdReq { get; set; }
        public int IdPro { get; set; }
        public int IdSec { get; set; }
        public decimal QtdPro { get; set; }
        public decimal PreUnit { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ItemRequisicaoPutDTO
    {
        [Required(ErrorMessage = "A quantidade do produto é obrigatória.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo.")]
        public decimal QtdPro { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser um valor positivo.")]
        public decimal? PreUnit { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O total do item deve ser um valor positivo.")]
        public decimal? TotalItem { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O total real deve ser um valor positivo.")]
        public decimal? TotalReal { get; set; }
    }
}
