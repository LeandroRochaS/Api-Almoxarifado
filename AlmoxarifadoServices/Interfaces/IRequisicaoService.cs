using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IRequisicaoService : IServiceBase<Requisicao>
    {
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
    }
}
