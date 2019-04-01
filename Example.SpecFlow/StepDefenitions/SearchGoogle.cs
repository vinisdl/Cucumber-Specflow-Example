using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.SpecFlow.StepDefenitions.Helpers;
using Example.SpecFlow.StepDefenitions.Helpers.Components;

namespace Example.SpecFlow.StepDefenitions
{
    [Binding]
    public class SearchGoogle
    {
        [Given(@"Eu estou na home")]
        public void DadoEuEstouNoLogin()
        {
            NavigationHelper.NavigateToUrl("/");
        }

        [When(@"Clico em pesquisar")]
        public void QuandoClicoEm()
        {
            WebDriverHelper
            .Driver
            .FindElements(By.XPath("//*[@value='Pesquisa Google']"))[1].Click();
        }

         [Given(@"digito no campo de pesquisa ""(.*)""")]
        public void DigitoNoCampo(string texto)
        {
            TextBoxHelper.TypeInTextBox(By.Name("q"), texto);
        }

        [Then(@"Devo ver ""(.*)""")]
        public void EntaoDevoVer(string p0)
        {
            Assert.IsFalse(!WebDriverHelper.Driver.PageSource.Contains(p0));
        }
    }

}