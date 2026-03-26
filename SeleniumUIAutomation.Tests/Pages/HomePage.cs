using OpenQA.Selenium;

namespace SeleniumUIAutomation.Tests.Pages
{
    public class HomePage : BasePage
    {
        // Locators
        private readonly By _welcomeMessage = By.CssSelector(".welcome-message");
        private readonly By _userProfileIcon = By.Id("user-profile");
        private readonly By _logoutButton = By.Id("logout-button");
        private readonly By _searchInput = By.Id("search-input");
        private readonly By _searchButton = By.Id("search-button");
        private readonly By _navigationMenu = By.CssSelector(".nav-menu");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public string GetWelcomeMessage()
        {
            return GetText(_welcomeMessage);
        }

        public bool IsWelcomeMessageDisplayed()
        {
            return IsElementDisplayed(_welcomeMessage);
        }

        public void ClickUserProfile()
        {
            Click(_userProfileIcon);
        }

        public void ClickLogout()
        {
            Click(_logoutButton);
        }

        public void SearchFor(string searchTerm)
        {
            SendKeys(_searchInput, searchTerm);
            Click(_searchButton);
        }

        public bool IsNavigationMenuDisplayed()
        {
            return IsElementDisplayed(_navigationMenu);
        }

        public bool IsHomePageDisplayed()
        {
            return IsWelcomeMessageDisplayed() && IsNavigationMenuDisplayed();
        }
    }
}
