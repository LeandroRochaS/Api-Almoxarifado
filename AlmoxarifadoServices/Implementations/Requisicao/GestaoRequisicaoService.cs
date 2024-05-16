using AlmoxarifadoAPI.Models;
using AlmoxarifadoDomain.Models;
using AlmoxarifadoServices.Interfaces;
using AlmoxarifadoServices.ViewModels.ItemRequisicao;
using AlmoxarifadoServices.ViewModels.Requisicao;

namespace AlmoxarifadoServices.Implementations
{
    public class GestaoRequisicaoService : IGestaoRequisicaoService
    {
        private readonly IItemRequisicaoService _itemRequisicaoService;
        private readonly IRequisicaoService _requisicaoService;
        private readonly ISetorService _setorService;
        private readonly IClienteService _clienteService;
        private readonly ISecretariaService _secretariaService;
        private readonly IProdutoService _produtoService;
        private readonly IEstoqueService _estoqueService;
 
        public GestaoRequisicaoService(IItemRequisicaoService itemRequisicaoService, IRequisicaoService requisicaoService, ISetorService setorService, IClienteService clienteService, ISecretariaService secretariaService, IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _itemRequisicaoService = itemRequisicaoService;
            _requisicaoService = requisicaoService;
            _setorService = setorService;
            _clienteService = clienteService;
            _secretariaService = secretariaService;
            _produtoService = produtoService;
            _estoqueService = estoqueService;
        }

        public async Task<ItensReq> RegistrarItemRequisicao(int id, CreateItemRequisicaoViewModel itemRequisicaoView)
        {
            try
            {
                if (await ExisteRequisicao(id))
                {
                    if (await VerificarRelacionamentosItemReq(id, itemRequisicaoView))
                    {
                        decimal totalItem = itemRequisicaoView.QtdPro * itemRequisicaoView.PreUnit;
                        ItensReq itemRequisicao = CriarItemRequisicao(id, itemRequisicaoView, totalItem);

                        if (await VerificarEstoqueSuficiente(itemRequisicao.IdPro, itemRequisicao.QtdPro))
                        {
                            var resultItem = await _itemRequisicaoService.Create(itemRequisicao);
                            if (resultItem != null)
                            {
                                await AtualizarEstoque(itemRequisicao.IdPro, itemRequisicaoView.QtdPro);
                                await VerificarEstoqueMinimo(itemRequisicao.IdPro, itemRequisicao.IdSec, itemRequisicao.IdReq);
                                return resultItem;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Quantidade de produto em estoque insuficiente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        private async Task VerificarEstoqueMinimo(int idPro, int idSec, int idReq)
        {
            var produto = await _produtoService.GetById(idPro);
            var estoqueProduto = await _estoqueService.GetById(idPro);
            if (estoqueProduto.QtdPro <= produto.EstoqueMin)
            {
                LogEstoqueCriticoService.CriarLogCSV(new LogEstoqueMinimo
                {
                    IdProduto = idPro,
                    IdRequisicao = idReq,
                    IdSecretaria = idSec,
                    DataRegistro = DateTime.Now,
                    QuantidadeAtual = estoqueProduto.QtdPro
                });

            }
        }

        public async Task<Requisicao> RegistrarRequisicao(CreateRequisicaoViewModel requisicaoView)
        {
            try
            {
                if (await VerificarRelacionamentosRequisicao(requisicaoView))
                {
                    Requisicao requisicao = CriarRequisicao(requisicaoView);
                    return await _requisicaoService.Create(requisicao);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private async Task<bool> VerificarRelacionamentosRequisicao(CreateRequisicaoViewModel requisicao)
        {
            return requisicao.IdCli != 0 &&
                   requisicao.IdSec != 0 &&
                   requisicao.IdSet != 0 &&
                   await _clienteService.GetById(requisicao.IdCli) != null &&
                   await _setorService.GetById(requisicao.IdSet) != null &&
                   await _secretariaService.GetById(requisicao.IdSec) != null;
        }

        private async Task<bool> VerificarRelacionamentosItemReq(int Idreq, CreateItemRequisicaoViewModel itemReq)
        {
            return itemReq.IdPro != 0 &&
                   Idreq != 0 &&
                   itemReq.IdSec != 0 &&
                   await _requisicaoService.GetById(Idreq) != null &&
                   await _produtoService.GetById(itemReq.IdPro) != null &&
                   await _setorService.GetById(itemReq.IdSec) != null;
        }

        private async Task<bool> ExisteRequisicao(int id)
        {
            return await _requisicaoService.GetById(id) != null;
        }
        public async Task<bool> VerificarEstoqueSuficiente(int IdPro, decimal quantidadeSaida)
        {
            var produto = await _produtoService.GetById(IdPro);
            if (produto == null)
                return false;

            var estoque = await _estoqueService.GetById(produto.IdPro);
            if (estoque == null)
                return false;

            return quantidadeSaida <= estoque.QtdPro;
        }

        private async Task AtualizarEstoque(int IdPro, decimal quantidadeSaida)
        {
            await _estoqueService.RemoverEstoque(IdPro, quantidadeSaida);
        }

        private ItensReq CriarItemRequisicao(int id, CreateItemRequisicaoViewModel itemRequisicaoView, decimal totalItem)
        {
            return new ItensReq
            {
                IdPro = itemRequisicaoView.IdPro,
                IdReq = id,
                IdSec = itemRequisicaoView.IdSec,
                PreUnit = itemRequisicaoView.PreUnit,
                NumItem = itemRequisicaoView.NumItem,
                QtdPro = itemRequisicaoView.QtdPro,
                TotalItem = totalItem,
                TotalReal = totalItem
            };
        }

        private Requisicao CriarRequisicao(CreateRequisicaoViewModel requisicaoView)
        {
            return new Requisicao
            {
                Ano = requisicaoView.Ano,
                DataReq = DateTime.Now,
                IdCli = requisicaoView.IdCli,
                IdSec = requisicaoView.IdSec,
                IdSet = requisicaoView.IdSet,
                Mes = requisicaoView.Mes,
                Observacao = requisicaoView.Observacao,
                TotalReq = 0,
                QtdIten = 0
            };
        }
    }
}
