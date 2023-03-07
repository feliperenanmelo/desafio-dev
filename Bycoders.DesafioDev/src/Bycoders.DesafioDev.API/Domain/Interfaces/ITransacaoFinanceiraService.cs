using Bycoders.DesafioDev.API.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface ITransacaoFinanceiraService
    {
        Task<Paginacao<TipoTransacaoResponse>> GetAllTipoTransacao(int pageSize, int page);
        Task<TransacoesFinanceirasResponse> CreateByPathFile(IFormFile file);
        Task<Paginacao<TransacaoFinanceiraResponse>> GetAll(int pageSize, int page);
    }
}
