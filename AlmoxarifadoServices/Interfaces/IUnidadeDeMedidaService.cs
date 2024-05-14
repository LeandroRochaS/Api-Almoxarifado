﻿using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IUnidadeDeMedidaService
    {
        Task<UnidadeMedidum> GetById(int id);
    }
}
