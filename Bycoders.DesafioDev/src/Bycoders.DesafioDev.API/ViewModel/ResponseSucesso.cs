using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class Response
    {
        [JsonProperty("sucesso")]
        public bool Sucesso { get; set; }

        [JsonProperty("dados")]
        public object Dados { get; set; }
        
        [JsonProperty("erros")]
        public List<string> Erros { get; set; }
    }
}
