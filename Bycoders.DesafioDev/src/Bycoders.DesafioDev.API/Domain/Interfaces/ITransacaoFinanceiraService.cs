using Bycoders.DesafioDev.API.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface ITransacaoFinanceiraService
    {
        Task<Paginacao<TipoTransacaoResponse>> ObterTodosTiposTransacao(int pageSize, int page);
        Task<TransacoesFinanceirasResponse> CriarPorArquivo(IFormFile file);
        Task<Paginacao<TransacaoFinanceiraResponse>> ObterTodos(int pageSize, int page);
    }
}
