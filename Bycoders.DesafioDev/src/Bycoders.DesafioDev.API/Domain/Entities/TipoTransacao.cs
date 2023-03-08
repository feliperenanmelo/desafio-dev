using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.Domain.Entities
{
    public class TipoTransacao : BaseEntity
    {
        public string Descricao { get; set; }
        public string Natureza { get; set; }
        public char Sinal { get; set; }

        public List<TransacaoFinanceira> TransacaosFinanceiras { get; } = new List<TransacaoFinanceira>();

        protected TipoTransacao()
        { }

        private TipoTransacao(string descricao, string natureza, char sinal)
        {
            Descricao = descricao;
            Natureza = natureza;
            Sinal = sinal;
        }

        public static TipoTransacao Create(string descricao, string natureza, char sinal)
            => new (descricao, natureza, sinal);
    }
}
