using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public partial class Estoque
    {
        public int IdSec { get; set; }
        public int IdPro { get; set; }
        public decimal QtdPro { get; set; }


        public bool VerificarEstoqueSuficiente(decimal quantidade)
        {
            if (QtdPro >= quantidade)
            {
                return true;
            }
            return false;
        }

        public void RemoverEstoque(decimal quantidade)
        {
            QtdPro -= quantidade;
        }

        public void AdicionarEstoque(decimal quantidade)
        {
            QtdPro += quantidade;
        }
    }
}
