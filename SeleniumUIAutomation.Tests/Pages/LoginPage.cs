using OpenQA.Selenium;

namespace SeleniumUIAutomation.Tests.Pages
{
    public class LoginPage : BasePage
    {
        // Locators
        private readonly By _usernameInput = By.Id("username");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _errorMessage = By.CssSelector(".error-message");
        private readonly By _forgotPasswordLink = By.LinkText("Forgot Password?");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToLoginPage(string url)
        {
            NavigateTo(url);
        }

        public void EnterUsername(string username)
        {
            SendKeys(_usernameInput, username);
        }

        public void EnterPassword(string password)
        {
            SendKeys(_passwordInput, password);
        }

        public void ClickLoginButton()
        {
            Click(_loginButton);
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsElementDisplayed(_errorMessage);
        }

        public void ClickForgotPassword()
        {
            Click(_forgotPasswordLink);
        }

        public bool IsLoginPageDisplayed()
        {
            return IsElementDisplayed(_usernameInput) && IsElementDisplayed(_passwordInput);
        }
    }
}
