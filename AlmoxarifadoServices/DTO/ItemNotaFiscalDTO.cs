
using System.ComponentModel.DataAnnotations;

namespace AlmoxarifadoServices.DTO
{
    public class ItemNotaFiscalPostDTO
    {

        [Required(ErrorMessage = "O número do item é obrigatório.")]
        public int ItemNum { get; set; }

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

    public class ItemNotaFiscalGetDTO
    {
        public int IdNota { get; set; }
        public int IdPro { get; set; }
        public int IdSec { get; set; }
        public string ItemNum { get; set; } = null!;
        public decimal QtdPro { get; set; }
        public decimal PreUnit { get; set; }
        public decimal TotalItem { get; set; }


    }

    public class ItemNotaFiscalPutDTO
    {
        [Required(ErrorMessage = "A quantidade do produto é obrigatória.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo.")]
        public decimal QtdPro { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser um valor positivo.")]
        public decimal PreUnit { get; set; }
    }

}
