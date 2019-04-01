using Example.SpecFlow.StepDefenitions.Helpers;
using ReportPortal.Shared;
using ReportPortal.SpecFlowPlugin;
using ReportPortal.SpecFlowPlugin.EventArguments;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;

namespace Example.SpecFlow.Hooks
{
    
    [Binding]
    public sealed class Hook
    {

         [BeforeScenario]
        public void BeforeScenario()
        {
            WebDriverHelper.Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), GetChromeOptions());
        }

         private static ChromeOptions GetChromeOptions()
        {
            var option = new ChromeOptions();
            option.AddArguments("headless");

            option.AddArguments("start-maximized"); // open Browser in maximized mode
            option.AddArguments("disable-infobars"); // disabling infobars
            option.AddArguments("--disable-extensions"); // disabling extensions
            option.AddArguments("--disable-gpu"); // applicable to windows os only
            option.AddArguments("--disable-dev-shm-usage"); // overcome limited resource problems
            option.AddArguments("--no-sandbox"); // Bypass OS security model
            return option;
        }

        // BeforeTestRun hook order should be set to the value that is lower than -20000
        // if you plan to use ReportPortalAddin.BeforeRunStarted event.
        [BeforeTestRun(Order = -30000)]
        public static void BeforeTestRunPart()
        {
            ReportPortalAddin.BeforeRunStarted += ReportPortalAddin_BeforeRunStarted;
            ReportPortalAddin.BeforeFeatureStarted += ReportPortalAddin_BeforeFeatureStarted;
            ReportPortalAddin.BeforeScenarioStarted += ReportPortalAddin_BeforeScenarioStarted;
            ReportPortalAddin.BeforeScenarioFinished += ReportPortalAddin_BeforeScenarioFinished;
        }

        private static void ReportPortalAddin_BeforeRunStarted(object sender, RunStartedEventArgs e)
        {
            e.StartLaunchRequest.Description = $"OS: {Environment.OSVersion.VersionString}";
        }

        private static void ReportPortalAddin_BeforeScenarioFinished(object sender, TestItemFinishedEventArgs e)
        {
            if (e.ScenarioContext.TestError != null && e.ScenarioContext.ScenarioInfo.Title == "System Error")
            {
                e.FinishTestItemRequest.Issue = new ReportPortal.Client.Models.Issue
                {
                    Type = ReportPortal.Client.Models.WellKnownIssueType.SystemIssue,
                    Comment = "my custom system error comment"
                };
            }
        }

        private static void ReportPortalAddin_BeforeFeatureStarted(object sender, TestItemStartedEventArgs e)
        {
            // Adding feature tag on runtime
            e.StartTestItemRequest.Tags.Add("runtime_feature_tag");
        }

        private static void ReportPortalAddin_BeforeScenarioStarted(object sender, TestItemStartedEventArgs e)
        {
            // Adding scenario tag on runtime
            e.StartTestItemRequest.Tags.Add("runtime_scenario_tag");
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            if (context.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                GenericHelper.TakeScreenShot(context.TestError.ToString());

            WebDriverHelper.Driver.Quit();
        }
    }
}
