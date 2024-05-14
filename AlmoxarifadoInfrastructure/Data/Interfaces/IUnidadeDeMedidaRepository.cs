using AlmoxarifadoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data.Interfaces
{
    public interface IUnidadeDeMedidaRepository
    {

        Task<UnidadeMedidum> GetById(int id);
    }
}
