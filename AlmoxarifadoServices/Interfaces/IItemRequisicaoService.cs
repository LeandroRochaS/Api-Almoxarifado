using AlmoxarifadoAPI.Models;
<<<<<<< Updated upstream
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
    public interface IItemRequisicaoService : IServiceBase<ItensReq>
    {
<<<<<<< Updated upstream
=======
        Task<ItemRequisicaoGetDTO> Create(int id, ItemRequisicaoPostDTO itemRequisicaoView);
        Task<ItemRequisicaoGetDTO> Update(int id, ItemRequisicaoPutDTO itemRequisicao);
        Task<ItemRequisicaoGetDTO> GetById(int id);
        Task<ItemRequisicaoGetDTO> Delete(int id);
        Task<IEnumerable<ItemRequisicaoGetDTO>> GetAll();
>>>>>>> Stashed changes
    }
}
