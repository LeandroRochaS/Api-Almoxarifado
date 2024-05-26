using AlmoxarifadoAPI.Models;

namespace AlmoxarifadoServices.Interfaces
{
    public interface IEstoqueService
    {

        Task<Estoque> Create(Estoque entity);
        Task<Estoque> Delete(int id, int idSec);
        Task<IEnumerable<Estoque>> GetAll();
        Task<Estoque> GetById(int id, int idSec);
        Task<Estoque> Update(Estoque entity);
        Task<bool> VerificarEstoqueSuficiente(
                            int IdPro,
                            int IdSec,
                            decimal QtdPro);
        Task AtualizarEstoque(int id, int idSec, decimal quantidade, bool adicionar);


    }
}
