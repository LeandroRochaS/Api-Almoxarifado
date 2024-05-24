
namespace AlmoxarifadoServices.DTO
{
    public class ItemNotaFiscalPostDTO
    {
        public int ItemNum { get; set; }
        public int IdPro { get; set; }
        public int IdSec { get; set; }
        public decimal QtdPro { get; set; }
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
        public decimal QtdPro { get; set; }
        public decimal PreUnit { get; set; }
    }

}
