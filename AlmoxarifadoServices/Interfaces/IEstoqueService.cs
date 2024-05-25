using AlmoxarifadoAPI.Models;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IEstoqueService
    {

        Task<Estoque> Create(Estoque entity);
        Task<Estoque> Delete(int id, int idSec);
        Task<IEnumerable<Estoque>> GetAll();
        Task<Estoque> GetById(int id, int idSec);
        Task<Estoque> Update(int id, Estoque entity);
        Task<Estoque> AdicionarEstoque(int id, int idSec, decimal quantidade);
        Task<Estoque> RemoverEstoque(int id, int idSec, decimal quantidade);

        Task<bool> VerificarEstoqueSuficiente(
                            int IdPro,
                            int IdSec,
                            decimal QtdPro);

    }
}
