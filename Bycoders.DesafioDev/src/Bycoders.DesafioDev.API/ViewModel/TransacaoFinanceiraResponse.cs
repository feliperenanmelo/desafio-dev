using Newtonsoft.Json;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class TransacaoFinanceiraResponse : IResponse
    {
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("tipo_transacao")]
        public TipoTransacaoResponse TipoTransacao { get; private set; }

        [JsonProperty("dia")]
        public string Data { get; private set; }

        [JsonProperty("cpf")]
        public string CPF { get; private set; }

        [JsonProperty("cartao")]
        public string Cartao { get; private set; }
        
        [JsonProperty("hora")]
        public string Hora { get; private set; }

        [JsonProperty("nome_dono")]
        public string Dono { get; private set; }

        [JsonProperty("nome_loja")]
        public string NomeLoja { get; private set; }

        [JsonProperty("valor")]
        public decimal Valor { get; private set; }

        private TransacaoFinanceiraResponse()
        { }

        public TransacaoFinanceiraResponse(
            int id,
            TipoTransacaoResponse tipoTransacao,
            string data,
            string cpf,
            string cartao,
            string hora,
            string dono,
            string nomeLoja,
            decimal valor)
        {
            Id = id;
            TipoTransacao = tipoTransacao;
            Data = data;
            CPF = cpf;
            Cartao = cartao;
            Hora = hora;
            Dono = dono;
            NomeLoja = nomeLoja;
            Valor = valor;
        }

        public static TransacaoFinanceiraResponse Create(int id,
            TipoTransacaoResponse tipoTransacao,
            string data,
            string cpf,
            string cartao,
            string hora,
            string dono,
            string nomeLoja,
            decimal valor) => new (id, tipoTransacao, data, cpf, cartao, hora, dono, nomeLoja, valor);        
    }
}
