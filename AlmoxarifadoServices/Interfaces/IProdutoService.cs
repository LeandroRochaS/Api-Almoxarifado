using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IProdutoService 
    {
        Task<IEnumerable<ProdutoGetDTO>> GetAll(); 
        Task<ProdutoGetDTO> Create(ProdutoPostDTO entity);
        Task<ProdutoGetDTO> Update(int id, ProdutoPutDTO entity);
        Task<ProdutoGetDTO> Delete(int id);
        Task<ProdutoGetDTO> GetById(int id);

    }
}
