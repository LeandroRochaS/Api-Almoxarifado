
using AlmoxarifadoDomain.Models;

namespace AlmoxarifadoServices.Implementations
{
    public class LogEstoqueCriticoService
    {
         
        public static void CriarLogCSV(LogEstoqueMinimo logEstoque)
        {
            // Criação do arquivo CSV
            var dataString = logEstoque.DataRegistro.ToString().Replace("/", "-").Replace("/", "-").Replace(":", "-").Replace(":", "-").Trim();
            string path = @$"C:\Users\Leandro\Documents\Códigos\Senai\Api-Almoxarifado\Logs\produtosAbaixoMinimoE_{dataString}_{logEstoque.IdRequisicao}.csv";
            using (StreamWriter sw = new StreamWriter(path))
            {
                // Cabeçalho
                sw.WriteLine("IdProduto;IdSecretaria;IdRequisicao;QuantidadeAtual;DataRegistro");

              
                    sw.WriteLine($"{logEstoque.IdProduto};{logEstoque.IdSecretaria};{logEstoque.IdRequisicao};{logEstoque.QuantidadeAtual};{logEstoque.DataRegistro}");
                
            }
        }
    }
}
