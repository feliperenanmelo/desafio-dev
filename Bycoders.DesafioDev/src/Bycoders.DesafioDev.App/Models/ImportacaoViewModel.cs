using Microsoft.AspNetCore.Http;

namespace Bycoders.DesafioDev.App.Models
{
    public class ImportacaoViewModel
    {
        public IFormFile File { get; set; }
        public string Erro { get; set; }
    }
}
