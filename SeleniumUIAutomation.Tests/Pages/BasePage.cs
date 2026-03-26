using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumUIAutomation.Tests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        protected IWebElement FindElement(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        protected void Click(By locator)
        {
            var element = Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        protected void SendKeys(By locator, string text)
        {
            var element = FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return FindElement(locator).Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return FindElement(locator).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected void WaitForElementToDisappear(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        protected void SelectDropdownByText(By locator, string text)
        {
            var dropdown = new SelectElement(FindElement(locator));
            dropdown.SelectByText(text);
        }

        protected void SelectDropdownByValue(By locator, string value)
        {
            var dropdown = new SelectElement(FindElement(locator));
            dropdown.SelectByValue(value);
        }

        protected void ScrollToElement(By locator)
        {
            var element = FindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        protected void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        protected string GetPageTitle()
        {
            return Driver.Title;
        }

        protected string GetCurrentUrl()
        {
            return Driver.Url;
        }

        protected void SwitchToFrame(By locator)
        {
            var frame = FindElement(locator);
            Driver.SwitchTo().Frame(frame);
        }

        protected void SwitchToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
        }

        protected void AcceptAlert()
        {
            Wait.Until(ExpectedConditions.AlertIsPresent());
            Driver.SwitchTo().Alert().Accept();
        }

        protected void DismissAlert()
        {
            Wait.Until(ExpectedConditions.AlertIsPresent());
            Driver.SwitchTo().Alert().Dismiss();
        }

        protected string GetAlertText()
        {
            var alert = Wait.Until(ExpectedConditions.AlertIsPresent());
            return alert?.Text ?? string.Empty;
        }
    }
}
