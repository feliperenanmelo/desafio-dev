using Bycoders.DesafioDev.Tests.Configurations;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Bycoders.DesafioDev.Tests.Automatizados
{
    public class ImportaoArquivoTests
    {
        [Fact(DisplayName = "Não permitir importação sem arquivo sucesso")]
        [Trait("Controller", "ImportacaoController")]
        public void DadoQueClicarEmImportar_QuandoNaoImportarArquivo_NaoPermitirImportar()
        {
            // Arrange
            var browser = new Selenium(Browser.Chrome, new SeleniumConfiguracoes(), false);

            // Act
            browser.IrParaUrl($"{browser.Configuracoes.UrlBaseApp}{browser.Configuracoes.Importacao}");
            browser.ClicarBotaoPorId("importar");
            var erroTitulo = browser.ObterTextElementoPorId("msgRetorno");
            var erro = browser.ObterTextElementoPorId("erro");
            
            browser.Dispose();

            // Assert
            Assert.True(erroTitulo.Equals("Opa! Algo deu errado :("));
            Assert.True(erro.Equals("Arquivo inexistente ou maior que 1Mb"));
            
        }

        [Fact(DisplayName = "Não permitir importação de arquivo com extensão diferente de txt")]
        [Trait("Controller", "ImportacaoController")]
        public void DadoQueClicarEmImportar_QuandoImportouArquivoComextensaoDiferenteDeTxt_NaoPermitirImportar()
        {
            // Arrange
            var browser = new Selenium(Browser.Chrome, new SeleniumConfiguracoes(), false);
            var path = Path.Combine(AppContext.BaseDirectory, browser.Configuracoes.ArquivoInvalido);

            // Act
            browser.IrParaUrl($"{browser.Configuracoes.UrlBaseApp}{browser.Configuracoes.Importacao}");
            browser.PreencherInputPorId("txt", path);
            browser.ClicarBotaoPorId("importar");
            
            var erroTitulo = browser.ObterTextElementoPorId("msgRetorno");
            var erro = browser.ObterTextElementoPorId("erro");
            
            browser.Dispose();

            // Assert
            Assert.True(erroTitulo.Equals("Opa! Algo deu errado :("));
            Assert.True(erro.Equals("Arquivo com extensão inválida"));
        }

        [Fact(DisplayName = "Importação sucesso")]
        [Trait("Controller", "ImportacaoController")]
        public void DadoQueClicarEmImportar_QuandoImportouArquivoTxtValido_ImportarComSucesso()
        {
            // Arrange
            var browser = new Selenium(Browser.Chrome, new SeleniumConfiguracoes(), false);
            var path = Path.Combine(AppContext.BaseDirectory, browser.Configuracoes.ArquivoCorreto);

            // Act
            browser.IrParaUrl($"{browser.Configuracoes.UrlBaseApp}{browser.Configuracoes.Importacao}");
            browser.PreencherInputPorId("txt", path);
            browser.ClicarBotaoPorId("importar");

            var erroTitulo = browser.ValidarSeElementoExistePorId("msgRetorno");
            var erro = browser.ValidarSeElementoExistePorId("erro");

            var tabela = browser.ObterElementoPorId("tabela");
            var cabecalhos = browser.ObterElementosPorClass("cabecalho").Select(elemento => elemento.Text).ToList();
            var corpos = browser.ObterElementosPorClass("corpo").Select(elemento => elemento.Text).ToList();

            browser.Dispose();

            // Assert
            Assert.False(erroTitulo);
            Assert.False(erro);
            Assert.NotNull(tabela);

            Assert.True(cabecalhos.Exists(cab => cab.Equals("Loja", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(cabecalhos.Exists(cab => cab.Equals("Total de Importações", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(cabecalhos.Exists(cab => cab.Equals("Saldo", StringComparison.CurrentCultureIgnoreCase)));

            Assert.True(corpos.Exists(ca => ca.Equals("BAR DO JOÃO", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(corpos.Exists(ca => ca.Equals("3", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(corpos.Exists(ca => ca.Equals("-102.00", StringComparison.CurrentCultureIgnoreCase)));            
        }

        [Fact(DisplayName = "Importação sucesso")]
        [Trait("Controller", "ImportacaoController")]
        public void DadoQueClicarEmImportar_QuandoImportouArquivoTxtValido_ImportarCodmSucesso()
        {
            // Arrange
            var browser = new Selenium(Browser.Chrome, new SeleniumConfiguracoes(), false);
            var path = Path.Combine(AppContext.BaseDirectory, browser.Configuracoes.ArquivoCorreto);

            // Act
            browser.IrParaUrl($"{browser.Configuracoes.UrlBaseApp}{browser.Configuracoes.Importacao}");
            browser.PreencherInputPorId("txt", path);
            browser.ClicarBotaoPorId("importar");

            var erroTitulo = browser.ValidarSeElementoExistePorId("msgRetorno");
            var erro = browser.ValidarSeElementoExistePorId("erro");

            var tabela = browser.ObterElementoPorId("tabela");
            var cabecalhos = browser.ObterElementosPorClass("cabecalho").Select(elemento => elemento.Text).ToList();
            var corpos = browser.ObterElementosPorClass("corpo").Select(elemento => elemento.Text).ToList();

            browser.Dispose();

            // Assert
            Assert.False(erroTitulo);
            Assert.False(erro);
            Assert.NotNull(tabela);

            Assert.True(cabecalhos.Exists(cab => cab.Equals("Loja", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(cabecalhos.Exists(cab => cab.Equals("Total de Importações", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(cabecalhos.Exists(cab => cab.Equals("Saldo", StringComparison.CurrentCultureIgnoreCase)));

            Assert.True(corpos.Exists(ca => ca.Equals("BAR DO JOÃO", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(corpos.Exists(ca => ca.Equals("3", StringComparison.CurrentCultureIgnoreCase)));
            Assert.True(corpos.Exists(ca => ca.Equals("-102.00", StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
