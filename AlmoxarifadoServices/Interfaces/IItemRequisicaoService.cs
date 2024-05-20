using AlmoxarifadoAPI.Models;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using AlmoxarifadoServices.DTO;
>>>>>>> Stashed changes

namespace AlmoxarifadoServices.Interfaces
{
    public interface IItemRequisicaoService 
    {
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
        Task<ItemRequisicaoGetDTO> Create(int id, ItemRequisicaoPostDTO itemRequisicaoView);
        Task<ItemRequisicaoGetDTO> Update(int id, ItemRequisicaoPutDTO itemRequisicao);
        Task<ItemRequisicaoGetDTO> GetById(int id);
        Task<ItemRequisicaoGetDTO> Delete(int id);
        Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll();
>>>>>>> Stashed changes
=======
        Task<ItensReq> Create(int id, CreateItemRequisicaoViewModel itemRequisicaoView);
        Task<ItensReq> Update(int id, ItensReq itemRequisicao);
        Task<ItensReq> GetById(int id);
        Task<ItensReq> Delete(int id);
        Task<IEnumerable<ItensReq>> GetAll();
>>>>>>> 30e6dd1030f4b35a99494c3f0dde13c4ced4d96e
    }
}
