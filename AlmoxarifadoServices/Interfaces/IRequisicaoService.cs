using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
using AlmoxarifadoServices.ViewModels.Requisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IRequisicaoService 
    {
<<<<<<< HEAD
=======
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IRequisicaoService
    {
        Task<RequisicaoGetDTO> Create(RequisicaoPostDTO requisicaoView);
        Task<RequisicaoGetDTO> Delete(int id);
        Task<IEnumerable<RequisicaoGetDTO>> GetAll();
        Task<RequisicaoGetDTO> GetById(int id);
        Task<RequisicaoGetDTO> Update(int id, RequisicaoPostDTO requisicaoView);
>>>>>>> Stashed changes
=======

        Task<Requisicao> Create(CreateRequisicaoViewModel requisicaoView);
        Task<Requisicao> Update(int id, Requisicao entity);
        Task<Requisicao> GetById(int id);
        Task<IEnumerable<Requisicao>> GetAll();
        Task<Requisicao> Delete(int id);
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
