using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.ViewModel;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface ITransacaoFinanceiraService
    {
        Task<Paginacao<TipoTransacaoResponse>> GetAllTipoTransacao(int pageSize, int page);
        Task<TransacoesFinanceirasResponse> CreateByPathFile(string path, string fileName);
        Task<Paginacao<TransacaoFinanceiraResponse>> GetAll(int pageSize, int page);
    }
}
