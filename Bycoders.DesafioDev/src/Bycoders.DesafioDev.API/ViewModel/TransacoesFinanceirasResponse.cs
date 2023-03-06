using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class TransacoesFinanceirasResponse : IResponse
    {
        [JsonProperty("transacoes_inseridas")]
        public List<TransacaoFinanceiraResponse> TransacaoFinanceirasSucesso { get; set; } = new List<TransacaoFinanceiraResponse>();

        [JsonProperty("transacoes_nao_processadas")]
        public List<TransacaoFinanceiraErroResponse> TransacaoFinanceirasComErro { get; set; } = new List<TransacaoFinanceiraErroResponse>();
    }

    public class TransacaoFinanceiraErroResponse
    {
        [JsonProperty("linha_nao_processada")]
        public int Linha { get; private set; }

        [JsonProperty("transacao_nao_processada")]
        public TransacaoFinanceiraResponse TransacaoFinanceira { get; private set; }

        [JsonProperty("erros")]
        public IEnumerable<string> Erros { get; set; }

        private TransacaoFinanceiraErroResponse(
            int linha,
            TransacaoFinanceiraResponse transacaoFinanceira,
            IEnumerable<string> erros)
        {
            Linha = linha;
            TransacaoFinanceira = transacaoFinanceira;
            Erros = erros;
        }

        private TransacaoFinanceiraErroResponse()
        { }

        public static TransacaoFinanceiraErroResponse Create(int posicao, TransacaoFinanceiraResponse transacaoFinanceira, IEnumerable<string> erros)
            => new TransacaoFinanceiraErroResponse(posicao, transacaoFinanceira, erros);
    }
}
