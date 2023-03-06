using System;

namespace Bycoders.DesafioDev.API.Domain.Entities
{
    public class TransacaoFinanceira : BaseEntity
    {
        public int TipoTransacaoId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public DateTime Data { get; set; }
        public string CPF { get; set; }
        public string Cartao { get; set; }
        public string Hora { get; set; }
        public string Dono { get; set; }
        public string NomeLoja { get; set; }

        private decimal _valor;
        public decimal Valor
        {
            get => _valor;
            set => _valor = value / 100.00m;
        }

        protected TransacaoFinanceira() { }

        private TransacaoFinanceira(
            int tipoTransacaoId,
            DateTime data,
            string cpf,
            string cartao,
            string hora,
            string dono,
            string nomeLoja,
            decimal valor)
        {
            TipoTransacaoId = tipoTransacaoId;
            Data = data;
            CPF = cpf;
            Cartao = cartao;
            Hora = hora;
            Dono = dono;
            NomeLoja = nomeLoja;
            Valor = valor;
        }

        public void IncluirTipoTransacao(TipoTransacao tipoTransacao)
        {
            TipoTransacao = tipoTransacao;
        }

        public static TransacaoFinanceira Create(int tipoTransacaoId, DateTime data, string cpf, string cartao, string hora, string dono, string nomeLoja, decimal valor)
            => new(tipoTransacaoId, data, cpf, cartao, hora, dono, nomeLoja, valor);
    }
}
