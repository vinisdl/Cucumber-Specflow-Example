using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Example.SpecFlow.StepDefenitions.Helpers;
using Example.SpecFlow.StepDefenitions.Helpers.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Example.SpecFlow.StepDefenitions.Helpers {
    public class GenericHelper {
        public static IWebElement GetElement (By locator) {
            if (IsElemetPresent (locator))
                return WebDriverHelper.Driver.FindElement (locator);

            throw new NoSuchElementException ("Element Not Found : " + locator.ToString ());
        }

        public static bool IsElemetPresent (By locator) {
            try {
                return WebDriverHelper.Driver.FindElements (locator).Count == 1;
            } catch (Exception) {
                return false;
            }
        }

        public static void TakeScreenShot (string filename = "Screen") {
            var screen = WebDriverHelper.Driver.TakeScreenshot ();
            if (filename.Equals ("Screen")) {
                filename = filename + DateTime.UtcNow.ToString ("yyyy-MM-dd-mm-ss") + ".jpeg";
                screen.SaveAsFile (filename, ScreenshotImageFormat.Jpeg);
                return;
            }
            screen.SaveAsFile (filename, ScreenshotImageFormat.Jpeg);
        }
    }
}