# Aquality Selenium for .NET

## Package description:
This package is a library designed to simplify your work with Selenium WebDriver.<br>
You've got to use this set of methods, related to most common actions performed with web elements.<br>
Most of performed methods are logged using NLog, so you can easily see a history of performed actions in your log.<br>
We use interfaces where is possible, so you can implement your own version of target interface with no need to rewrite other classes.<br>
The solution constructed to be used with PageObjects pattern, but it is not necessary requirement. You can read more about PageObjects approach on various sources, e.g. here: http://www.assertselenium.com/automation-design-practices/page-object-pattern/
<br>
To start work with this package, simply download latest stable version of `Aquality.Selenium` from NuGet  

## How to use:
You can take a look at our integration test "ShoppingCartTest" and related classes as an example of typical usage of the package.<br>
Step-by-step guide:<br>
1) Get instance of Browser:
```csharp
Browser browser = BrowserManager.Browser;
```
Use Browser's methods directly for general actions, such as navigation, window resize, scrolling and alerts handling:
```csharp
  browser.Maximize();
  browser.Navigate().GoToUrl("https://wikipedia.org");
  browser.WaitForPageToLoad();
  browser.HandleAlert(AlertAction.Accept);
  browser.ScrollWindowBy(x: 0, y: 1000);
  browser.Navigate().Back();
  browser.Navigate().Refresh();
  browser.ExecuteScript(JavaScript.AutoAcceptAlerts)
  browser.Quit();
```
2) Create separate form classes for each page, modal, dialog and popup you're working with.
- Inherit your form classes from Form class to use predefined methods; 
- Always specify unique page locator and its name in constructor.
```csharp
public class MainPage : Form 
{
    public MainPage() : base(By.XPath("//h3[contains(., 'Main')]"), "Main") 
    {
    }
```
3) Add elements you want to interact within the current form as private properties. Use ElementFactory property to get an instance of each element.
```csharp
    private ITextBox EmailTxb => ElementFactory.GetTextBox(By.Id("email_create"), "Email Create");
    private IButton ProductsBtn => ElementFactory.GetButton(By.XPath("//span[@class='ajax_cart_product_txt_s']"), "Products");
    private ICheckBox NewsChbx => ElementFactory.GetCheckBox(By.Id("newsletter"), "newsletter", ElementState.ExistsInAnyState);
```
- As you can see, an instance of `ElementFactory` is created for each form inherited from Form; you can create your own instance of `ElementFactory` whether you need it; you can extend `ElementFactory` class for your purposes, e.g. for your custom elements.

If an element has locator dependent on parameter, you can store locator template as string (See javadocs <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated">$"//*[contains(., '{partial_text}')]"</a>) in private constant at form class. 

Do the same if you aim to find multiple elements: 
```csharp
    private static readonly string GenderRdbtnTemplate = "//input[@name='id_gender'][@value='{0}']";
    private static readonly string XPathSelectDays = "//select[@id='days']/option";
```
4) Create methods related to business actions performed on the page, or to get information from the page: 
```csharp
public void SetFirstName(string firstName)
{
    firstNameTxb.Type(firstName);
}

public void OpenCartMenu()
{
    productsBtn.MouseActions.MoveMouseToElement();
}

public int GetNumOfDays()
{
    var daysLbl = ElementFactory.FindElements<ILabel>(By.XPath(XPathSelectDays));
    return lblDays.Count;
}

public bool IsNewsCheckboxSelected()
{
    return newsChbx.IsChecked;
}
```

If element's locator depend on external param, you can create it inside the method:
```csharp
public void SelectGender(int genderId)
{
    var genderRdbtn = ElementFactory.GetRadioButton(By.XPath(string.Format(GenderRdbtnTemplate, genderId)), $"Gender Id {genderId}");
    rbGender.Click();
}
```
5) Finally, create an instance of your form and perform required actions:
```csharp
  var authForm = new AuthenticationForm();
  authForm.SetEmail(userEmail);
  authForm.clickCreateAccount();
  var personalInfoForm = new YourPersonalInfoForm();
  personalInfoForm.SelectGender(genderId);
  personalInfoForm.SetFirstName(userFirstName);
```


## Use several browser instances in parallel (multithreading):
In our library instances of some core classes (Browser, BrowserManager, Logger, Configuration) are stored in thread-local containers.
You may want to interact with more than one instance of Browser. For this purpose, you will need to create browser instances in separate threads, to make sure that their work does not interrupt each other.<br>
You can take a look at our test class BrowserConcurrencyTests for example:
```csharp
```


## Library structure: 
- **Aquality.Selenium**:
    - **Browsers**: classes and methods to setup and interract with browser
        *BrowserManager* allows you to setup a browser instance.  <br>
          It is implemented using [WebDriverManager](https://github.com/bonigarcia/webdrivermanager). This manager allows you to get required version of target browser's webdriver.
        - There are an interface *IBrowserFactory* and two default implementations *LocalBrowserFactory* and *RemoteBrowserFactory* in this namespace.
        - If you need to specify a concrete version of WebDriver to be used, you can implement your own implementation of *IBrowserFactory*. Please use static method *BrowserManager.SetFactory(IBrowserFactory webDriverFactory)* so set usage of your implementation.
        - *Browser* is a wrapper to interact with Selenium WebDriver. It contains the most common methods to interact with browser: setup, navigate, resize, scroll, handle alerts etc.
    - **Configurations**: read configuration from .json files. Please see <a href='#configuration'>Configuration</a> section for details.
    - **Elements**: functionality designed to simplify work with web elements.
      - **Actions**: functionality related to JavaScript and Mouse actions performed under web elements.
      - **Interfaces**: common interfaces to easily interact with webelements via specific wrappers: *IButton, ICheckBox, IComboBox, ILabel, ILink, IRadioButton* and *ITextBox*.        
      -- *ElementFactory*: class designed to create instances of element wrappers
      You can implement your own type of element by extending *Element* class. If you prefer to create your own interface, it should implement *IElement* interface.
    - **Forms**: contains *Form* class.
    - **Logging**: contains Logger designed to log performed actions.
    - **Waitings**: contains ConditionalWait class with functionality for conditional waitings.
    - **Resources**:
      - **JavaScripts**: JS functions used within this package.
      - **Localization**: JSON files with localized values for logging.
      - *settings.json*: default library settings.


## Configuration
We store configuration parameters in `settings.json` file.<br>
You can find default configuration in the `Resources` folder.<br>
To change some parameter, you have to add related resource file into your project and change the required parameter.<br>
Also you can define several configurations by adding files with appropriate suffixes like `settings.{config_name}.json` and then pass this suffix into environment variable with name `profile`.<br>
Make sure to add it to the same path where it is presented in library.<br>
Alternatively, you can override some class from the ``` namespace Aquality.Selenium.Configurations  ```.

#### Configuration file content description

###### General properties
- "browserName": "chrome" - defines default browser to run tests.
- "isRemote": true - defines if the remote WebDriver instance will be created. Set "true" to use Selenium Grid.
- "remoteConnectionUrl": "http://localhost:4444/wd/hub" - URL of the remote web driver.
- "isElementHighlightEnabled": true - defines whether to highlight elements during actions or not. "Highlight" is made of red border, which is added to each interacted element using specific JavaScript. Set "false" if you want to switch off this feature.

###### driverSettings
Threre are separate object with settings in this section for each supported browser which is mapped to appropriate `IDriverSettings` implementation.<br>
Each settings object consists of:<br>
- "webDriverVersion": "Latest" - version of web driver for [WebDriverManager](https://github.com/bonigarcia/webdrivermanager).<br>
- "capabilities": {} - you can read more about desired capabilities at [SeleniumHQ wiki](https://github.com/SeleniumHQ/selenium/wiki/DesiredCapabilities).<br>
Values from this object are set to specific Options object (e.g. ChromeOptions, FirefoxOptions etc.) with command `options.SetCapability(key, value);`<br>
- "options": {} - values from this file are set to specific Options object (e.g. ChromeOptions, FirefoxOptions etc.) with command `options.SetExperimentalOption("prefs", chromePrefs);`<br>
- "startArguments": {} - values from this object are set to specific Options object (e.g. ChromeOptions, FirefoxOptions etc.) with command `options.AddArguments(arg);`<br>

Afterwards this Settings object is passed as parameter to WebDriver constructor.

###### timeouts
- "timeoutImplicit": 0 - implicit wait timeout in seconds. We do not recommend to set non-zero value to it. Instead of implicit wait you can take advantage of ConditionalWait.
- "timeoutCondition": 30 - timeout in seconds for waiting actions.
- "timeoutScript": 10 - timeout in seconds for script execution.
- "timeoutPageLoad": 15 - page loading timeout in seconds.
- "timeoutPollingInterval": 300 - retry interval in milliseconds for waiting actions.
- "timeoutCommand": 60 - command timeout in seconds for remote web driver.

###### retry (generally uses to handle StaleElementReferenceException)
- "number": 2 - default number of retries for some actions. 
- "pollingInterval": 300 - interval in milliseconds between retries.

###### logger
- "language": "en" - default language for log messages.