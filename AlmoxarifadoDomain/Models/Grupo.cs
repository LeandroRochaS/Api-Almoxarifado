using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Subgrupos = new HashSet<Subgrupo>();
        }

        public int IdGru { get; set; }
        public string NomeGru { get; set; } = null!;
        public string? SugestaoGru { get; set; }

        public virtual ICollection<Subgrupo> Subgrupos { get; set; }
    }
}
