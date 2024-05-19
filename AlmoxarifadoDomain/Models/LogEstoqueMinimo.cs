using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoDomain.Models
{
    public class LogEstoqueMinimo
    {

        public int IdProduto { get; set; }
        public int IdSecretaria { get; set; }
        public int IdRequisicao { get; set; }
        public decimal QuantidadeAtual { get; set; }

        public DateTime DataRegistro { get; set; }

        public LogEstoqueMinimo() { }
    }
}
