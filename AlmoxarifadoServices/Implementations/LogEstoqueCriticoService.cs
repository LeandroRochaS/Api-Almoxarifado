
using AlmoxarifadoDomain.Models;
using AlmoxarifadoServices.Interfaces;

namespace AlmoxarifadoServices.Implementations
{
    public class LogEstoqueCriticoService 
    {
        public static void CriarLogCSV(LogEstoqueMinimo logEstoque)
        {
            string folderPath = "Logs"; 

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var dataString = logEstoque.DataRegistro.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = Path.Combine(folderPath, $"produtosAbaixoMinimoE_{dataString}_{logEstoque.IdRequisicao}.csv");

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("IdProduto;IdSecretaria;IdRequisicao;QuantidadeAtual;DataRegistro");

                sw.WriteLine($"{logEstoque.IdProduto};{logEstoque.IdSecretaria};{logEstoque.IdRequisicao};{logEstoque.QuantidadeAtual};{logEstoque.DataRegistro}");
            }
        }
    }
}
