using OpenQA.Selenium;

namespace SeleniumUIAutomation.Tests.Utilities
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string screenshotDirectory = Path.Combine(projectDirectory, "Reports", "Screenshots");

            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string screenshotPath = Path.Combine(screenshotDirectory, $"{screenshotName}_{timestamp}.png");

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);

            return screenshotPath;
        }

        public static string CaptureScreenshotAsBase64(IWebDriver driver)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }
    }
}
