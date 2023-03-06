using Newtonsoft.Json;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class TipoTransacaoResponse : IResponse
    {
        [JsonProperty("descricao_transacao")]
        public string Descricao { get; set; }

        [JsonProperty("natureza_transacao")]
        public string Natureza { get; set; }

        [JsonProperty("sinal")]
        public char Sinal { get; set; }

        private TipoTransacaoResponse()
        { }

        private TipoTransacaoResponse(string descricao, string natureza, char sinal)
        {
            Descricao = descricao;
            Natureza = natureza;
            Sinal = sinal;
        }

        public static TipoTransacaoResponse Create(string descricao, string natureza, char sinal)
            => new TipoTransacaoResponse(descricao, natureza, sinal);
    }
}
