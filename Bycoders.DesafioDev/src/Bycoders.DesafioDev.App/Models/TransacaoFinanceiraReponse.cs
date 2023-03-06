using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bycoders.DesafioDev.App.Models
{
    public class Reponse<T> where T : class
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class TransacoesFinanceirasResponse 
    {
        [JsonProperty("transacoes_inseridas")]
        public List<TransacaoFinanceiraResponse> TransacaoFinanceirasSucesso { get; set; } = new List<TransacaoFinanceiraResponse>();

        [JsonProperty("transacoes_nao_processadas")]
        public List<TransacaoFinanceiraErroResponse> TransacaoFinanceirasComErro { get; set; } = new List<TransacaoFinanceiraErroResponse>();
    }

    public class TransacaoFinanceiraResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tipo_transacao")]
        public TipoTransacaoResponse TipoTransacao { get; set; }

        [JsonProperty("dia")]
        public string Data { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("cartao")]
        public string Cartao { get; set; }

        [JsonProperty("hora")]
        public string Hora { get; set; }
        
        [JsonProperty("nome_dono")]
        public string Dono { get; set; }

        [JsonProperty("nome_loja")]
        public string NomeLoja { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }
    }

    public class TipoTransacaoResponse
    {
        [JsonProperty("descricao_transacao")]
        public string Descricao { get; set; }

        [JsonProperty("natureza_transacao")]
        public string Natureza { get; set; }

        [JsonProperty("sinal")]
        public string Sinal { get; set; }
    }

    public class TransacaoFinanceiraErroResponse
    {
        [JsonProperty("linha_nao_processada")]
        public int Linha { get; private set; }

        [JsonProperty("transacao_nao_processada")]
        public TransacaoFinanceiraResponse TransacaoFinanceira { get; private set; }

        [JsonProperty("erros")]
        public IEnumerable<string> Erros { get; set; }
    }
}