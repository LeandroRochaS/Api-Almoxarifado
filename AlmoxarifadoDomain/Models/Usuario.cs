using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public partial class Usuario
    {
        public int IdUsu { get; set; }
        public string NomeUsu { get; set; } = null!;
        public string LogonUsu { get; set; } = null!;
        public string SenhaUsu { get; set; } = null!;
        public string TipoUsu { get; set; } = null!;
    }
}
