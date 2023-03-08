using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class ViewModelExtensions
    {
        public static TransacaoFinanceiraResponse ParaTransacaoFinanceiraResponse(this TransacaoFinanceira transacaoFinanceira)
        {
            return TransacaoFinanceiraResponse
                .Create(transacaoFinanceira.Id, transacaoFinanceira.TipoTransacao?.ParaTipoTransacaoResponse(), transacaoFinanceira.Data.ToString("yyyy-MM-dd"), transacaoFinanceira.CPF,
                transacaoFinanceira.Cartao, transacaoFinanceira.Hora, transacaoFinanceira.Dono, transacaoFinanceira.NomeLoja, transacaoFinanceira.Valor);
        }

        public static List<TransacaoFinanceiraResponse> ParaTransacaoFinanceiraResponse(this IEnumerable<TransacaoFinanceira> transacoes)
        {
            var transacoesReponse = new List<TransacaoFinanceiraResponse>();

            transacoes?.ToList()?.ForEach(transacao 
                => transacoesReponse.Add(transacao?.ParaTransacaoFinanceiraResponse()));

            return transacoesReponse;
        }

        public static TipoTransacaoResponse ParaTipoTransacaoResponse(this TipoTransacao tipoTransacao)
        {
            return TipoTransacaoResponse.Create(tipoTransacao.Descricao, tipoTransacao.Natureza, tipoTransacao.Sinal);            
        }

        public static List<TipoTransacaoResponse> ParaTipoTransacaoResponse(this IEnumerable<TipoTransacao> tiposTransacao)
        {
            var tiposTransacaoResponse = new List<TipoTransacaoResponse>();

            tiposTransacao?.ToList()?.ForEach(tipoTransacao
                => tiposTransacaoResponse.Add(tipoTransacao?.ParaTipoTransacaoResponse()));

            return tiposTransacaoResponse;
        }
    }
}
