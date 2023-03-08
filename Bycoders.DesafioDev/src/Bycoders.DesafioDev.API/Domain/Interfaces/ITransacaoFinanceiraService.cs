using Bycoders.DesafioDev.API.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface ITransacaoFinanceiraService
    {   
        Task<TransacoesFinanceirasResponse> CriarPorArquivo(IFormFile file);        
    }
}
