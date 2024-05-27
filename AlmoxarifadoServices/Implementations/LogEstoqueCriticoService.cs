
using AlmoxarifadoDomain.Models;

namespace SeuProjeto.Services.Implementations
{
    public class LogEstoqueCriticoService 
    {
        private static string _logDirectory = "Logs";

        public static void CriarOuAtualizarLogCSV(LogEstoqueMinimo logEstoque)
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            string existingFilePath = FindExistingFilePath(logEstoque.IdRequisicao.ToString());
            bool fileExists = !string.IsNullOrEmpty(existingFilePath);

            string filePath;
            if (fileExists)
            {
                filePath = existingFilePath;
            }
            else
            {
                var dataString = logEstoque.DataRegistro.ToString("yyyy-MM-dd_HH-mm-ss");
                filePath = Path.Combine(_logDirectory, $"produtosAbaixoMinimoEm_{dataString}_{logEstoque.IdRequisicao}.csv");
            }

            if (!fileExists)
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("IdProduto;IdSecretaria;IdRequisicao;QuantidadeAtual;DataRegistro");
                }
            }

            if (ProdutoJaRegistradoNoArquivo(filePath, logEstoque.IdProduto.ToString()))
            {
                AtualizarProdutoNoArquivo(filePath, logEstoque);
            }
            else
            {
                AdicionarProdutoNoArquivo(filePath, logEstoque);
            }
        }

        private static string FindExistingFilePath(string requisitionId)
        {
            var files = Directory.GetFiles(_logDirectory, $"*_{requisitionId}.csv");
            return files.Length > 0 ? files[0] : null;
        }

        private static bool ProdutoJaRegistradoNoArquivo(string filePath, string idProduto)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var columns = line.Split(';');
                    if (columns.Length > 0 && columns[0] == idProduto)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void AdicionarProdutoNoArquivo(string filePath, LogEstoqueMinimo logEstoque)
        {
            using (StreamWriter sw = new StreamWriter(filePath, append: true))
            {
                sw.WriteLine($"{logEstoque.IdProduto};{logEstoque.IdSecretaria};{logEstoque.IdRequisicao};{logEstoque.QuantidadeAtual};{logEstoque.DataRegistro}");
            }
        }

        private static void AtualizarProdutoNoArquivo(string filePath, LogEstoqueMinimo logEstoque)
        {
            var tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(filePath))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var columns = line.Split(';');
                    if (columns.Length > 0 && columns[0] == logEstoque.IdProduto.ToString())
                    {
                        sw.WriteLine($"{logEstoque.IdProduto};{logEstoque.IdSecretaria};{logEstoque.IdRequisicao};{logEstoque.QuantidadeAtual};{logEstoque.DataRegistro}");
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            File.Delete(filePath);
            File.Move(tempFile, filePath);
        }
    }
}
