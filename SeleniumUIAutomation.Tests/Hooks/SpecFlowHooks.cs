using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumUIAutomation.Tests.Drivers;
using SeleniumUIAutomation.Tests.Utilities;
using TechTalk.SpecFlow;

namespace SeleniumUIAutomation.Tests.Hooks
{
    [Binding]
    public class SpecFlowHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private ExtentTest? _test;
        private static ExtentTest? _featureTest;

        public SpecFlowHooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportManager.GetInstance();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportManager.FlushReport();
            Console.WriteLine($"Report generated at: {ExtentReportManager.GetReportPath()}");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureTest = ExtentReportManager.CreateTest(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            string browser = ConfigurationManager.Browser;
            bool headless = ConfigurationManager.Headless;

            if (string.IsNullOrEmpty(browser))
            {
                browser = "chrome";
            }

            DriverManager.InitializeDriver(browser, headless);

            _test = _featureTest?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(_scenarioContext.ScenarioInfo.Title);
            _scenarioContext["ExtentTest"] = _test;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (_scenarioContext.TestError != null)
                {
                    var driver = DriverManager.Driver;
                    string screenshotBase64 = ScreenshotHelper.CaptureScreenshotAsBase64(driver);

                    _test?.Fail(_scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotBase64).Build());
                }
            }
            catch (Exception ex)
            {
                _test?.Warning($"Could not capture screenshot: {ex.Message}");
            }
            finally
            {
                //DriverManager.QuitDriver();
            }
        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = _scenarioContext.StepContext.StepInfo.Text;

            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Given>(stepText).Pass("");
                        break;
                    case "When":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.When>(stepText).Pass("");
                        break;
                    case "Then":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Then>(stepText).Pass("");
                        break;
                    case "And":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.And>(stepText).Pass("");
                        break;
                    case "But":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.But>(stepText).Pass("");
                        break;
                }
            }
            else
            {
                switch (stepType)
                {
                    case "Given":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Given>(stepText).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "When":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.When>(stepText).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "Then":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.Then>(stepText).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "And":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.And>(stepText).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "But":
                        _test?.CreateNode<AventStack.ExtentReports.Gherkin.Model.But>(stepText).Fail(_scenarioContext.TestError.Message);
                        break;
                }
            }
        }
    }
}
