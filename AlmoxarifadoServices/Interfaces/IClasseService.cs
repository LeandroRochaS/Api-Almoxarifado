﻿using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IClasseService
    {
        Task<Classe> GetById(int id);
    }
}
