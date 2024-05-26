namespace AlmoxarifadoServices.DTO
{
    public class ItemRequisicaoPostDTO
    {
        public int NumItem { get; set; }
        public int IdPro { get; set; }
        public int IdSec { get; set; }
        public decimal QtdPro { get; set; }
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
        public decimal QtdPro { get; set; }
        public decimal? PreUnit { get; set; }
        public decimal? TotalItem { get; set; }
        public decimal? TotalReal { get; set; }
    }
}
