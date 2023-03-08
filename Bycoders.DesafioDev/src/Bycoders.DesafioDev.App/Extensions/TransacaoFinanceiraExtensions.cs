using Bycoders.DesafioDev.App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Bycoders.DesafioDev.App.Extensions
{
    public static class TransacaoFinanceiraExtensions
    {
        public static List<OperacaoSucessoPorLoja> ParaListaOperacoesSucessoPorLoja(this List<TransacaoFinanceiraResponse> transacoesFinanceiraSucessoResponse)
        {
            return transacoesFinanceiraSucessoResponse
                .GroupBy(tran => tran.NomeLoja)
                .Select(tran => new OperacaoSucessoPorLoja
                {
                    NomeLoja = tran.Key,
                    Transacoes = tran.Select(t => t).ToList()
                }).ToList();
        }

        public static List<OperacaoErroPorLoja> ParaListaOperacoesErroPorLoja(this List<TransacaoFinanceiraErroResponse> transacoesFinanceiraErros)
        {
            var transacoes = transacoesFinanceiraErros
               .GroupBy(tran => tran.TransacaoFinanceira.NomeLoja)
               .Select(tran =>
               new
               {
                   NomeLoja = tran.Key,
                   ErroPorLoja = tran.Select(tra =>
                           new
                           {
                               tra.Linha,
                               Erros = tra.Erros.ToList()
                           })
               })
               .Select(tran => 
                    new OperacaoErroPorLoja(
                        tran.NomeLoja,
                        tran.ErroPorLoja.ToDictionary(tr => tr.Linha, t => t.Erros)))
               .ToList();

            return transacoes;
        }
    }
}
