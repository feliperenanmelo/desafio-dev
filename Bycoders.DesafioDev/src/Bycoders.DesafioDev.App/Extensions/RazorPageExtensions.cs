using Bycoders.DesafioDev.App.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Bycoders.DesafioDev.App.Extensions
{
    public static class RazorPageExtensions
    {
        public static string ObterErro(this RazorPage page, List<string> erros)
        {
            var erro = string.Empty;            

            foreach (var er in erros)
            {
                erro += er + ", ";
            }
            return erro.Trim().TrimEnd(',');
        }

        public static void ObterErrosPorLoja(this RazorPage page, List<TransacaoFinanceiraErroResponse> transacoesFinanceirasErroResponse)
        {
            var transacoes = transacoesFinanceirasErroResponse
                .GroupBy(tran => tran.TransacaoFinanceira.NomeLoja)
                .Select(tran => new
                {
                    NomeLoja = tran.Key,
                    Erros = tran.Select(tran => new { tran.Linha, tran.Erros }),
                }).ToList();
                
        }

    }
}
