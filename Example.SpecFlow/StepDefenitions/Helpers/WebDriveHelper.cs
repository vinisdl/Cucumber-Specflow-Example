using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;


namespace Example.SpecFlow.StepDefenitions.Helpers
{
    public class WebDriverHelper
    {
        public static string DefaultUrl = "http://www.google.com";
        public static IWebDriver Driver { get; set; }
    }
}