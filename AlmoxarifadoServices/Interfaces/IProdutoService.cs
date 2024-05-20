using AlmoxarifadoAPI.Models;
using AlmoxarifadoServices.DTO;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {

        Task<Produto> CreateV2(ProdutoPostDTO entity);
    }
}
