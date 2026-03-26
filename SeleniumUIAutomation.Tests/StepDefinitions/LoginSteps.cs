using SeleniumUIAutomation.Tests.Drivers;
using SeleniumUIAutomation.Tests.Pages;
using SeleniumUIAutomation.Tests.Utilities;
using TechTalk.SpecFlow;

namespace SeleniumUIAutomation.Tests.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;
        private readonly ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(DriverManager.Driver);
            _homePage = new HomePage(DriverManager.Driver);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            string baseUrl = ConfigurationManager.BaseUrl;
            if (string.IsNullOrEmpty(baseUrl))
            {
                baseUrl = "https://uat.qality.dev/#/auth/login";
            }
            _loginPage.NavigateToLoginPage(baseUrl);
        }

        [When(@"I enter username ""(.*)""")]
        public void WhenIEnterUsername(string username)
        {
            _loginPage.EnterUsername(username);
        }

        [When(@"I enter password ""(.*)""")]
        public void WhenIEnterPassword(string password)
        {
            _loginPage.EnterPassword(password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
        }

        [Then(@"I should be redirected to the home page")]
        public void ThenIShouldBeRedirectedToTheHomePage()
        {
            Assert.That(_homePage.IsHomePageDisplayed(), Is.True, "Home page should be displayed after successful login");
        }

        [Then(@"I should see a welcome message")]
        public void ThenIShouldSeeAWelcomeMessage()
        {
            Assert.That(_homePage.IsWelcomeMessageDisplayed(), Is.True, "Welcome message should be displayed");
        }

        [Then(@"I should see an error message ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedMessage)
        {
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.True, "Error message should be displayed");
            string actualMessage = _loginPage.GetErrorMessage();
            Assert.That(actualMessage, Does.Contain(expectedMessage), $"Error message should contain: {expectedMessage}");
        }
    }
}
