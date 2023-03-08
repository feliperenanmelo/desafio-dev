using Bycoders.DesafioDev.App.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.App.Services
{
    public interface IImportaHttpRepository
    {
        Task<Reponse<TransacoesFinanceirasResponse>> PostAsync(IFormFile file);
    }
}
