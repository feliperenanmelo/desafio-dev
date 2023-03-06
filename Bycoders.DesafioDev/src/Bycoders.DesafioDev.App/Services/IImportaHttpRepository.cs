using Bycoders.DesafioDev.App.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.App.Services
{
    public interface IImportaHttpRepository
    {
        Task<Reponse<TransacoesFinanceirasResponse>> PostAsync(IFormFile file);
    }

    public class ImportaHttpRepository : IImportaHttpRepository
    {
        private readonly HttpClient _client;
        private readonly Appsettings _appsettings;

        public ImportaHttpRepository(
            HttpClient client,
            Appsettings appsettings)
        {
            _client = client;
            _appsettings = appsettings;
        }

        public async Task<Reponse<TransacoesFinanceirasResponse>> PostAsync(IFormFile file)
        {
            var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(file.OpenReadStream());

            content.Add(streamContent, file.Name, file.FileName);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = file.Name, FileName = file.FileName };

            var url = $"{_appsettings.ApiExterna.BaseUrl}/api/desafio-dev";

            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Reponse<TransacoesFinanceirasResponse>>(json);
            }

            return new Reponse<TransacoesFinanceirasResponse>();
        }

    }
}
