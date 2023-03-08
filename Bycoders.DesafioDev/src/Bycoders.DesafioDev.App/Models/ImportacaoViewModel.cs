using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Bycoders.DesafioDev.App.Models
{
    public class ImportacaoViewModel
    {
        public IFormFile File { get; set; }
        public List<string> Erros { get; set; } = new List<string>();
    }
}
