using OpenQA.Selenium;

namespace SeleniumUIAutomation.Tests.Pages
{
    public class LoginPage : BasePage
    {
        // Locators
        private readonly By _usernameInput = By.XPath("//input[@placeholder='Enter your username']");
        private readonly By _passwordInput = By.XPath("//input[@placeholder='Enter your password']");
        private readonly By _loginButton = By.XPath("//span[text()='Sign In']");
        private readonly By _errorMessage = By.XPath("//*[contains(text(),'invalid username or password') or contains(text(),'Invalid username or password') or contains(text(),'Please try again')]");
        private readonly By _forgotPasswordLink = By.LinkText("Forgot Password?");
        private static readonly By _errorMessageContainer = By.CssSelector(".error-message, .alert-error, .alert-danger, [role='alert']");
        private static readonly By _flashMessage = By.Id("flash");

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


        public string GetErrorMessageText()
        {
            try
            {
                if (IsElementDisplayed(_errorMessage))
                    return GetText(_errorMessage);
                if (IsElementDisplayed(_errorMessageContainer))
                    return GetText(_errorMessageContainer);
                if (IsElementDisplayed(_flashMessage))
                    return GetText(_flashMessage);
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get error message: {Message}", ex.Message);
                return string.Empty;
            }
        }
    }
}
