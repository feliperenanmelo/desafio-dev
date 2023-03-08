using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Bycoders.DesafioDev.Tests.Configurations
{
    public static class WebDriverFactory
    {
        public static IWebDriver CriarWebDriver(Browser browser, string caminhoDriver, bool mostrarAutomacaoTela)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Chrome:
                    var opcoes = new ChromeOptions();
                    if (mostrarAutomacaoTela)
                        opcoes.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, opcoes);
                    break;
                case Browser.Firefox:
                    throw new NotFoundException("Driver não implementado");
                default:
                    break;
            }

            return webDriver;
        }
    }
}
