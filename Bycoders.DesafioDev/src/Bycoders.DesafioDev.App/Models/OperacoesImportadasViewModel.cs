using System.Collections.Generic;
using System.Linq;

namespace Bycoders.DesafioDev.App.Models
{
    public class OperacoesImportadasViewModel
    {
        public List<OperacaoPorLoja> Operacoes { get; set; }
    }

    public class OperacaoPorLoja
    {
        public string NomeLoja { get; set; }
        public List<TransacaoFinanceiraResponse> Transacoes { get; set; }
        public string Saldo
        {
            get => ObterSaldo().ToString("N2");
        }

        private decimal ObterSaldo()
        {
            var saldoSomar = Transacoes.Where(tran => tran.TipoTransacao.Sinal.Equals("+")).Sum(tran => tran.Valor);
            
            var saldoSubtrair = Transacoes.Where(tran => tran.TipoTransacao.Sinal.Equals("-")).Sum(tran => tran.Valor);

            return saldoSomar + (saldoSubtrair * -1);
        }
    }
}
