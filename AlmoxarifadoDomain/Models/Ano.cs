using System;
using System.Collections.Generic;

namespace AlmoxarifadoAPI.Models
{
    public partial class Ano
    {
        public Ano()
        {
            MesCompetencia = new HashSet<MesCompetencium>();
        }
        
        public int Ano1 { get; set; }
        public bool? Aberto { get; set; }

        public virtual ICollection<MesCompetencium> MesCompetencia { get; set; }
    }
}
