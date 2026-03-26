using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace SeleniumUIAutomation.Tests.Utilities
{
    public class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static readonly object _lock = new object();
        private static string? _reportPath;

        public static ExtentReports GetInstance()
        {
            lock (_lock)
            {
                if (_extent == null)
                {
                    InitializeReport();
                }
                return _extent!;
            }
        }

        private static void InitializeReport()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string reportDirectory = Path.Combine(projectDirectory, "Reports");

            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            _reportPath = Path.Combine(reportDirectory, $"TestReport_{timestamp}.html");

            var sparkReporter = new ExtentSparkReporter(_reportPath);
            sparkReporter.Config.Theme = Theme.Standard;
            sparkReporter.Config.DocumentTitle = "Selenium UI Automation Report";
            sparkReporter.Config.ReportName = "Test Execution Report";
            sparkReporter.Config.Encoding = "UTF-8";

            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("Browser", "Chrome");
            _extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            _extent.AddSystemInfo("Machine", Environment.MachineName);
            _extent.AddSystemInfo("User", Environment.UserName);
        }

        public static ExtentTest CreateTest(string testName, string? description = null)
        {
            return GetInstance().CreateTest(testName, description);
        }

        public static void FlushReport()
        {
            lock (_lock)
            {
                _extent?.Flush();
            }
        }

        public static string? GetReportPath()
        {
            return _reportPath;
        }
    }
}
