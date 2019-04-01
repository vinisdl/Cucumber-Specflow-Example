namespace Example.SpecFlow.StepDefenitions.Helpers
{
    public class NavigationHelper
    {
        public static void NavigateToUrl(string path) => WebDriverHelper.Driver.Navigate().GoToUrl($"{WebDriverHelper.DefaultUrl}/{path}");
    }
}