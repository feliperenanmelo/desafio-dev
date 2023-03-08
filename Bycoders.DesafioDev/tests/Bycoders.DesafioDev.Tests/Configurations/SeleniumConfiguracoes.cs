using Microsoft.Extensions.Configuration;

namespace Bycoders.DesafioDev.Tests.Configurations
{
    public class SeleniumConfiguracoes
    {
        private readonly IConfiguration _configuracao;

        public SeleniumConfiguracoes()
        {
            _configuracao = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string WebDriver => _configuracao.GetSection("WebDriver").Value;
        public string UrlBaseApp => _configuracao.GetSection("UrlBaseApp").Value;
        public string Importacao => _configuracao.GetSection("Importacao").Value;
        public string ArquivoCorreto => _configuracao.GetSection("ArquivoCorreto").Value;
        public string ArquivoInvalido => _configuracao.GetSection("ArquivoInvalido").Value;
    }
}
