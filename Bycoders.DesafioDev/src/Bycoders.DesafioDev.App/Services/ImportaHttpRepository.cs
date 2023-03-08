using Bycoders.DesafioDev.App.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.App.Services
{
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
            var httpContent = new MultipartFormDataContent();

            var stream = new StreamContent(file.OpenReadStream());

            httpContent.Add(stream, file.Name, file.FileName);

            httpContent.Headers.ContentDisposition = 
                new ContentDispositionHeaderValue("form-data") { Name = file.Name, FileName = file.FileName };

            var url = $"{_appsettings.ApiExterna.BaseUrl}/api/desafio-dev";

            var resposta = await _client.PostAsync(url, httpContent);

            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Reponse<TransacoesFinanceirasResponse>>(json);
            }

            return new Reponse<TransacoesFinanceirasResponse>();
        }

    }
}
