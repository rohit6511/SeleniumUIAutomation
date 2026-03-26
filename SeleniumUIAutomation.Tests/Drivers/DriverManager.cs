using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumUIAutomation.Tests.Drivers
{
    public class DriverManager
    {
        private static IWebDriver? _driver;
        private static readonly object _lock = new object();

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("WebDriver has not been initialized. Call InitializeDriver first.");
                }
                return _driver;
            }
        }

        public static IWebDriver InitializeDriver(string browserType = "chrome", bool headless = false)
        {
            lock (_lock)
            {
                if (_driver != null)
                {
                    return _driver;
                }

                switch (browserType.ToLower())
                {
                    case "chrome":
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                        var chromeOptions = new ChromeOptions();
                        if (headless)
                        {
                            chromeOptions.AddArgument("--headless=new");
                        }
                        chromeOptions.AddArgument("--start-maximized");
                        chromeOptions.AddArgument("--disable-notifications");
                        chromeOptions.AddArgument("--disable-popup-blocking");
                        _driver = new ChromeDriver(chromeOptions);
                        break;

                    case "firefox":
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                        var firefoxOptions = new FirefoxOptions();
                        if (headless)
                        {
                            firefoxOptions.AddArgument("--headless");
                        }
                        _driver = new FirefoxDriver(firefoxOptions);
                        _driver.Manage().Window.Maximize();
                        break;

                    case "edge":
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                        var edgeOptions = new EdgeOptions();
                        if (headless)
                        {
                            edgeOptions.AddArgument("--headless=new");
                        }
                        edgeOptions.AddArgument("--start-maximized");
                        _driver = new EdgeDriver(edgeOptions);
                        break;

                    default:
                        throw new ArgumentException($"Browser type '{browserType}' is not supported.");
                }

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

                return _driver;
            }
        }

        public static void QuitDriver()
        {
            lock (_lock)
            {
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                    _driver = null;
                }
            }
        }
    }
}
