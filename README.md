# Selenium UI Automation Framework

A comprehensive UI automation framework built with C#, Selenium WebDriver, SpecFlow (BDD), NUnit, and Extent Reports.

## Project Structure

```
SeleniumUIAutomation/
├── SeleniumUIAutomation.Tests/
│   ├── Drivers/              # WebDriver management
│   │   └── DriverManager.cs
│   ├── Pages/                # Page Object Model classes
│   │   ├── BasePage.cs
│   │   ├── LoginPage.cs
│   │   └── HomePage.cs
│   ├── Features/             # SpecFlow feature files (Gherkin)
│   │   └── Login.feature
│   ├── StepDefinitions/      # SpecFlow step definitions
│   │   └── LoginSteps.cs
│   ├── Hooks/                # SpecFlow hooks for setup/teardown
│   │   └── SpecFlowHooks.cs
│   ├── Utilities/            # Helper classes
│   │   ├── ExtentReportManager.cs
│   │   ├── ScreenshotHelper.cs
│   │   ├── ConfigurationManager.cs
│   │   └── TestDataHelper.cs
│   ├── Reports/              # Generated test reports
│   ├── appsettings.json      # Configuration settings
│   └── specflow.json         # SpecFlow configuration
└── README.md
```

## Technologies Used

- **Selenium WebDriver 4.x** - Browser automation
- **SpecFlow 3.9** - BDD framework with Gherkin syntax
- **NUnit 4.x** - Test framework
- **Extent Reports 5.x** - HTML test reporting
- **WebDriverManager** - Automatic driver management
- **.NET 9.0** - Target framework

## Prerequisites

- .NET 9.0 SDK
- Chrome, Firefox, or Edge browser installed
- Visual Studio 2022 or VS Code with C# extension

## Getting Started

### 1. Clone or navigate to the project

```bash
cd SeleniumUIAutomation
```

### 2. Restore NuGet packages

```bash
dotnet restore
```

### 3. Build the solution

```bash
dotnet build
```

### 4. Run tests

```bash
# Run all tests
dotnet test

# Run tests with specific tags
dotnet test --filter "Category=smoke"

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## Configuration

Edit `appsettings.json` to configure test settings:

```json
{
  "TestSettings": {
    "BaseUrl": "https://your-app-url.com/login",
    "Browser": "chrome",
    "Headless": false,
    "ImplicitWaitSeconds": 10,
    "ExplicitWaitSeconds": 15
  }
}
```

### Supported Browsers

- `chrome` (default)
- `firefox`
- `edge`

## Writing Tests

### 1. Create a Feature File

```gherkin
@login
Feature: Login Functionality

@smoke
Scenario: Successful login
    Given I am on the login page
    When I enter username "user@example.com"
    And I enter password "Password123"
    And I click the login button
    Then I should be redirected to the home page
```

### 2. Create Page Object

```csharp
public class MyPage : BasePage
{
    private readonly By _myElement = By.Id("my-element");

    public MyPage(IWebDriver driver) : base(driver) { }

    public void ClickMyElement()
    {
        Click(_myElement);
    }
}
```

### 3. Create Step Definitions

```csharp
[Binding]
public class MySteps
{
    private readonly MyPage _myPage;

    public MySteps()
    {
        _myPage = new MyPage(DriverManager.Driver);
    }

    [When(@"I click my element")]
    public void WhenIClickMyElement()
    {
        _myPage.ClickMyElement();
    }
}
```

## Reports

After test execution, reports are generated in the `Reports` folder:
- **Extent Report**: `TestReport_[timestamp].html` - Detailed HTML report with screenshots on failure

## Page Object Model (POM)

The framework uses POM pattern for maintainability:

- **BasePage**: Contains common methods (Click, SendKeys, WaitForElement, etc.)
- **Page Classes**: Inherit from BasePage and define page-specific locators and methods

## Hooks

SpecFlow hooks handle test lifecycle:

- `BeforeTestRun`: Initialize Extent Reports
- `BeforeScenario`: Initialize WebDriver
- `AfterStep`: Log step results to report
- `AfterScenario`: Capture screenshots on failure, quit driver
- `AfterTestRun`: Flush reports

## Best Practices

1. **Locators**: Use stable locators (ID, data-testid) over XPath
2. **Waits**: Use explicit waits over implicit waits for specific conditions
3. **Page Objects**: Keep page classes focused and maintainable
4. **Test Data**: Use data-driven tests with Scenario Outline
5. **Tags**: Use tags for test categorization (@smoke, @regression)

## Troubleshooting

### Driver Issues
WebDriverManager handles driver downloads automatically. Ensure your browser is up-to-date.

### Timeout Issues
Increase wait times in `appsettings.json` if elements take longer to load.

### Headless Mode
Set `"Headless": true` in config for CI/CD pipelines.

## License

MIT License
