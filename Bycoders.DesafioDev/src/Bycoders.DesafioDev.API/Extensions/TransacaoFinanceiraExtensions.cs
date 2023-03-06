using Bycoders.DesafioDev.API.Configurations;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class TransacaoFinanceiraExtensions
    {
        public static string GetValue(this string line, CnabField cnabField)
        {
            var sizeLine = line.Length;

            var limit = sizeLine - (cnabField.Inicio);

            var tamanho = cnabField.Tamanho;

            if (limit < cnabField.Tamanho)
                tamanho = limit;

            var value = line.Substring(cnabField.Inicio - 1, tamanho);

            return value;
        }
    }
}
