﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.ViewModels.ItemNotaFiscal
{
    public class CreateItemNotaFiscalViewModel
    {

        public int ItemNum { get; set; }
        public int IdPro { get; set; }
        public int IdSec { get; set; }
        public decimal QtdPro { get; set; }
        public decimal PreUnit { get; set; }
    }
}