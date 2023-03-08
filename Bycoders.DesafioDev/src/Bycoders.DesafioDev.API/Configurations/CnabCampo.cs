using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.Configurations
{
    public class CnabConfiguracoes
    {
        public ICollection<CnabCampo> CnabCampos { get; set; }
    }

    public class CnabCampo
    {
        public DescricaoCampo Descricao { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public int Tamanho { get; set; }
        public TipoCampo TipoCampo { get; set; }
    }

    public enum DescricaoCampo
    {
        Tipo = 1,
        Data = 2,
        Valor = 3,
        CPF = 4,
        Cartao = 5,
        Hora = 6,
        DonoLoja = 7,
        NomeLoja = 8
    }

    public enum TipoCampo
    {
        Inteiro = 1,
        Decimal = 2,
        Datetime = 3,
        Caracter = 4,
        Texto = 5           
    }
}
