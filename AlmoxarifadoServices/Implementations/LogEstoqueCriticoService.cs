<<<<<<< HEAD
﻿using AlmoxarifadoDomain.Models;
=======
﻿using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e

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
