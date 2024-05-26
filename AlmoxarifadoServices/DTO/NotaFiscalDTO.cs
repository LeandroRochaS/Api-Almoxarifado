namespace AlmoxarifadoServices.DTO
{
    public class NotaFiscalComItensPostlDTO
    {
        public NotaFiscalPostDTO NotaFiscal { get; set; }
        public List<ItemNotaFiscalPostDTO> Itens { get; set; }
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
        public int QtdItem { get; set; }
        public string? ObservacaoNota { get; set; }
    }
    public class NotaFiscalPostDTO
    {
        public int IdFor { get; set; }
        public int IdSec { get; set; }
        public string NumNota { get; set; } = null!;
        public int QtdItem { get; set; }
        public int Ano { get; set; }
        public int? Mes { get; set; }
        public int IdTipoNota { get; set; }
        public string? ObservacaoNota { get; set; }
    }

    public class NotaFiscalComItensGetDTO
    {
        public NotaFiscalGetDTO NotaFiscal { get; set; }
        public List<ItemNotaFiscalGetDTO> Itens { get; set; }
    }
}
