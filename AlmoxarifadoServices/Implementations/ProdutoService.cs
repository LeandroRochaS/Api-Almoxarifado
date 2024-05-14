using AlmoxarifadoAPI.Models;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoServices.Implementations
{
    public class ProdutoService : IProdutoService
    {

        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Produto> CreateV2(CreateProdutoViewModel entity)
        {
            if (entity != null)
            {
                Produto produto = new Produto
                {
                    IdClas = entity.IdClasse,
                    IdUnMed = entity.IdUnMedida,
                    Descricao = entity.Descricao,
                    EstoqueMin = entity.EstoqueMin,
                    Observacao = entity.Observacao,
                    Perecivel = entity.Perecivel == AlmoxarifadoDomain.Enums.ProdutoPerecivelEnum.Sim ? true : false,
                    QtdEmbalagem = entity.QtdEmbalagem
                };


                var result = await _repository.Create(produto);
                return result;
            }
            return null;
        }

        public Task<Produto> Create(Produto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> Delete(int id)
        {
            var produtoDb = await _repository.GetById(id);
            if (produtoDb != null)
            {
                var result = await _repository.Delete(produtoDb);
                return result;
            }
            return produtoDb;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Produto> Update(int id, Produto entity)
        {
            var produtoDb = await _repository.GetById(id);
            if (produtoDb != null)
            {
                return await _repository.Update(entity);

            }

            return produtoDb;
        }
    }
}
