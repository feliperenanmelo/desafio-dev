using System.Collections.Generic;
using System.Linq;

namespace Bycoders.DesafioDev.App.Models
{
    public class OperacoesImportadasViewModel
    {
        public List<OperacaoSucessoPorLoja> OperacoesSucesso { get; set; } = new List<OperacaoSucessoPorLoja>();
        public List<OperacaoErroPorLoja> OperacoesNaoProcessadas { get; set; } = new List<OperacaoErroPorLoja>();       
    }

    public class OperacaoSucessoPorLoja
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

    public class OperacaoErroPorLoja
    {
        public string NomeLoja { get; set; }
        public Dictionary<int, List<string>> Erros { get; set; } = new Dictionary<int, List<string>>();

        public OperacaoErroPorLoja(string nomeLoja, Dictionary<int, List<string>> erros)
        {
            NomeLoja = nomeLoja;
            Erros = erros;
        }
    }
}
