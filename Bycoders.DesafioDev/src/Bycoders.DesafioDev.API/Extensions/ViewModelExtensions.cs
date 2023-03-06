using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class ViewModelExtensions
    {
        public static TransacaoFinanceiraResponse ToTransacaoFinanceiraResponse(this TransacaoFinanceira transacaoFinanceira)
        {
            return TransacaoFinanceiraResponse
                .Create(transacaoFinanceira.Id, transacaoFinanceira.TipoTransacao.ToTipoTransacaoResponse(), transacaoFinanceira.Data.ToString("yyyy-MM-dd"), transacaoFinanceira.CPF,
                transacaoFinanceira.Cartao, transacaoFinanceira.Hora, transacaoFinanceira.Dono, transacaoFinanceira.NomeLoja, transacaoFinanceira.Valor);
        }

        public static List<TransacaoFinanceiraResponse> ToTransacaoFinanceiraResponse(this IEnumerable<TransacaoFinanceira> transacoes)
        {
            var transacoesReponse = new List<TransacaoFinanceiraResponse>();

            transacoes.ToList().ForEach(transacao 
                => transacoesReponse.Add(transacao.ToTransacaoFinanceiraResponse()));

            return transacoesReponse;
        }

        public static TipoTransacaoResponse ToTipoTransacaoResponse(this TipoTransacao tipoTransacao)
        {
            return TipoTransacaoResponse.Create(tipoTransacao.Descricao, tipoTransacao.Natureza, tipoTransacao.Sinal);            
        }

        public static List<TipoTransacaoResponse> ToTipoTransacaoResponse(this IEnumerable<TipoTransacao> tiposTransacao)
        {
            var tiposTransacaoResponse = new List<TipoTransacaoResponse>();

            tiposTransacao.ToList().ForEach(tipoTransacao
                => tiposTransacaoResponse.Add(tipoTransacao.ToTipoTransacaoResponse()));

            return tiposTransacaoResponse;
        }
    }
}
