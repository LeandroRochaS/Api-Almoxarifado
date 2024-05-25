using AlmoxarifadoDomain.Enums;
using System.ComponentModel.DataAnnotations;


namespace AlmoxarifadoServices.DTO
{
    public class ProdutoPostDTO
    {
        [Required(ErrorMessage = "O campo Id da Classe é obrigatório.")]
        public int IdClasse { get; set; }

        [Required(ErrorMessage = "O campo Id da Unidade de Medida é obrigatório.")]
        public int IdUnMedida { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; } = null!;

        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O campo Estoque Mínimo é obrigatório.")]
        public int? EstoqueMin { get; set; }

        [Required(ErrorMessage = "O campo Perecível é obrigatório.")]
        public ProdutoPerecivelEnum Perecivel { get; set; }

        [Required(ErrorMessage = "O campo Quantidade por Embalagem é obrigatório.")]
        public int? QtdEmbalagem { get; set; }
    }

    public class ProdutoPutDTO
    {
        public string? Observacao { get; set; }
        public int? EstoqueMin { get; set; }
        public int? QtdEmbalagem { get; set; }
    }

    public class ProdutoGetDTO
    {
        public int IdPro { get; set; }
        public int IdClas { get; set; }
        public int IdUnMed { get; set; }
        public string Descricao { get; set; } = null!;
        public string? Observacao { get; set; }
        public int? EstoqueMin { get; set; }
        public bool? Perecivel { get; set; }
        public int? QtdEmbalagem { get; set; }
    }
}
