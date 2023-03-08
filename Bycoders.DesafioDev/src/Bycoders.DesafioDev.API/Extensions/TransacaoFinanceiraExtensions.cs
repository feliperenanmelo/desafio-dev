using Bycoders.DesafioDev.API.Configurations;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class TransacaoFinanceiraExtensions
    {
        public static string ObterValor(this string linha, CnabCampo cnabCampo)
        {
            var tamanhoLinha = linha.Length;

            var limite = tamanhoLinha - (cnabCampo.Inicio - 1);

            var posicaoBusca = cnabCampo.Tamanho;

            if (limite < cnabCampo.Tamanho)
                posicaoBusca = limite;

            var dados = linha.Substring(cnabCampo.Inicio - 1, posicaoBusca);

            return dados;
        }
    }
}
