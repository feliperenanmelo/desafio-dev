using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace Bycoders.DesafioDev.Tests.Configurations
{
    public class Selenium : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly SeleniumConfiguracoes Configuracoes;

        public WebDriverWait Aguardar;

        public Selenium(Browser browser, SeleniumConfiguracoes configuracoes, bool mostrarAutomacaoTela = true)
        {
            Configuracoes = configuracoes;

            WebDriver = WebDriverFactory.CriarWebDriver(browser, configuracoes.WebDriver, mostrarAutomacaoTela);

            WebDriver.Manage().Window.Maximize();

            Aguardar = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public void IrParaUrl(string url) => WebDriver.Navigate().GoToUrl(url);    

        public void ClicarBotaoPorId(string id)
        {
            var botao = Aguardar.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));

            botao.Click();
        }

        public void PreencherInputPorId(string id, string valor)
        {
            var elemento = WebDriver.FindElement(By.Id(id));

            elemento.SendKeys(valor);
        }

        public string ObterTextElementoPorId(string id)
        {
            var elemento = Aguardar.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));

            return elemento.Text;
        }

        public IWebElement ObterElementoPorId(string id)
        {
            var elemento = Aguardar.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));

            return elemento;
        }

        public IList<IWebElement> ObterElementosPorClass(string className)
        {
            var elemento = Aguardar.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));

            return elemento;
        }

        public bool ValidarSeElementoExistePorId(string id)
        {
            return ElementoExiste(By.Id(id));
        }

        private bool ElementoExiste(By por)
        {
            try
            {
                WebDriver.FindElement(por);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
