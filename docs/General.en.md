# Aquality Selenium for .NET

Aquality Selenium is a wrapper over Selenium WebDriver tool that allows to automate work with web browsers. Selenium WebDriver requires some skill and experience. So, Aquality Selenium suggests simplified and most importantly safer and more stable way to work with Selenium WebDriver.

 - <a href="#1-platform-support">1. PLATFORM SUPPORT</a>
 - <a href='#2-configurations'>2. CONFIGURATIONS</a>
    - <a href='#21-settings'>2.1. SETTINGS</a>
    - <a href='#22-browser'>2.2. BROWSER</a>
    - <a href='#23-driver-settings'>2.3. DRIVER SETTINGS</a>
    - <a href='#24-timeouts'>2.4. TIMEOUTS</a>
    - <a href='#25-retry-policy'>2.5. RETRY POLICY</a>
    - <a href='#26-logging'>2.6. LOGGING</a>
    - <a href='#27-cloud-usage'>2.7. CLOUD USAGE</a>
    - <a href='#28-actions-highlighting'>2.8. ACTIONS HIGHLIGHTING</a>
    - <a href='#29-access-from-the-code'>2.9. ACCESS FROM THE CODE</a>
 - <a href='#3-browser'>3. BROWSER</a>
    - <a href='#31-parallel-runs'>3.1. PARALLEL RUNS</a>
    - <a href='#32-browser-manager'>3.2. BROWSER MANAGER</a>
    - <a href='#33-browser-factory'>3.3. BROWSER FACTORY</a>
    - <a href='#34-driver-options'>3.4. DRIVER OPTIONS</a>
    - <a href='#35-download-directory'>3.5. DOWNLOAD DIRECTORY</a>
    - <a href='#36-alerts'>3.6. ALERTS</a>
    - <a href='#37-screenshots'>3.7. SCREENSHOTS</a>
 - <a href='#4-elements'>4. ELEMENTS</a>
    - <a href='#41-element-factory'>4.1. ELEMENT FACTORY</a>
    - <a href='#42-custom-elements'>4.2. CUSTOM ELEMENTS</a>
    - <a href='#43-list-of-elements'>4.3. LIST OF ELEMENTS</a>
    - <a href='#44-states-of-elements'>4.4. STATES OF ELEMENTS</a>
 - <a href='#5-forms'>5. FORMS</a>
 - <a href='#6-javascript-execution'>6. JAVASCRIPT EXECUTION</a>
 - <a href='#7-json-file'>7. JSON FILE</a>

### 1. PLATFORM SUPPORT

At the moment Aquality Selenium allows to automate web tests for Chrome, Firefox, Safari, IExplorer and Edge. Also you can implement support of new browsers that Selenium supports (more details [here](https://www.seleniumhq.org/about/platforms.jsp)).
Tests can be executed on any operating system with installed .NET Core SDK 2.1 and higher.

### 2. CONFIGURATIONS

Aquality Selenium provides flexible configuration to run tests by editing [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file. Most of the settings are clear without further explanation but major points are highlighted below. There is a possibility to implement your own configuration.

### 2.1. SETTINGS

The library uses [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file or its copies to store all necessary configurations for test runs. By default [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) from dll Embedded Resources is using. If you need to change some options from this file you have to create your own copy of this file in `Resource` folder in your project and change them in this copy. Also you can create several copies of this file with different settings and store in the same folder. The names of the copies should match the following pattern `settings.{profile}.json`. It is useful if you need to run tests on different operating systems, machines, browsers, etc. For example, the Aquality Selenium dev team has two configurations - [settings.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.json) and [settings.local.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.local.json) - to run tests in Circle CI docker container and on personal infrastructure. To change the settings you can set environment variable with name `profile` and value of desired settings (for example, `local`). By default file with name `settings.json` is using.

Any parameter from [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) also can be overridden through environment variables. You just need to set `jsonPath` to the desired parameter and its value in environment variable: `driverSettings.chrome.webDriverVersion: 77.0.3865.10`.

Settings file contains several sections the purpose of which is described below.

#### 2.2. BROWSER

`browserName` parameter defines the web browser which will be used for test run. For example, `browserName: chrome` means that tests will be run on Google Chrome.

`isRemote` parameter defines whether tests will be run on the same machine where .NET process is running or remote server will be using which is defined in parameter `remoteConnectionUrl`.

#### 2.3. DRIVER SETTINGS

Section `driverSettings` from [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) provides an ability to set up necessary capabilities, options and start arguments for web driver.

Please use official sources from web browser developers to get list of available arguments. For example, for Chrome: [run-chromium-with-flags](https://www.chromium.org/developers/how-tos/run-chromium-with-flags)

[Here](./IExplorer_Settings.md) we tried to described some points of work with IExplorer because of different information in the Internet.

#### 2.4. TIMEOUTS

[settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) contains `timeouts` section which includes a set of parameters related to different timeouts that are using in the library.

All these parameters are using to initialize object of [TimeoutConfiguration](../Aquality.Selenium/src/Aquality.Selenium/Configurations/TimeoutConfiguration.cs) which is accessible throught `Configuration.Instance.TimeoutConfiguration`.

The following are the parameters from `timeouts` section:

- `timeoutImplicit` = 0 seconds - web driver implicit wait timeout [Selenium Implicit Wait](https://www.seleniumhq.org/docs/04_webdriver_advanced.jsp#implicit-waits)
- `timeoutCondition` = 15 seconds - events with desired conditions timeout. Events include waiting for elements or their state
- `timeoutScript` = 10 seconds - it is a limit of scripts execution using WebDriver's method **ExecuteAsyncScript**
- `timeoutPageLoad` = 30 seconds - web page load timeout
- `timeoutPollingInterval` = 300 milliseconds - polling interval for explicit waits
- `timeoutCommand` = 60 seconds - maximum timeout of each WebDriver command

As part of the solution, all elements waits are met using Explicit Wait. The use of two wait types (implicit and explicit) is not recommended, as it can lead to incorrect behavior. So, the value of implicit wait will be set to zero forcibly before each explicit wait, regardless of what is in the configuration.

#### 2.5 RETRY POLICY

`retry` section from [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file is responsible for configuration the number of retries of actions on elements. All the actions such as clicking, typing, ect., can be performed repeatedly in case of exception. The [ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) class which is used for any actions on elements is responsible for this logic. 

The `number` parameter means the number of retries of action before exception is thrown. 

The `pollingInterval` parameter means the interval in milliseconds between the retries.

The [ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) handles `StaleElementReferenceException` and `InvalidElementStateException` by default and does the retries. 

#### 2.6. LOGGING

The solution supports logging of operations (interaction with the browser, page elements). Logging Example:

`2019-07-18 10:14:08 INFO  - Label 'First product' :: Moving mouse to element`

Supported languages:

- [en](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/en.json) - English
- [ru](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/ru.json) - Russian

The value of logging language is set in the [logger.language](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) parameter.

#### 2.7. CLOUD USAGE

To set up the run on the remote server with Selenium Grid (Selenoid, Zalenium) or on such platforms as BrowserStack, Saucelabs, etc., it is required to set correct value of URL for connection to the service in `remoteConnectionUrl` parameter in [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file. Also make sure that `isRemote` parameter has value **true**. For example, URL for BrowserStack can be the following [https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub](https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub).

#### 2.8. ACTIONS HIGHLIGHTING

`isElementHighlightEnabled` option is responsible for the need to highlight the elements of the web page with which the work is performed. Enabling the option allows you to more clearly observe the actions of the test.

#### 2.9. ACCESS FROM THE CODE

You can get an access to the settings using instance of [Configuration](../Aquality.Selenium/src/Aquality.Selenium/Configurations/Configuration.cs) class. For example:
```csharp
var browserName = Configuration.Instance.BrowserProfile.BrowserName;
```
This construction returns value of `browserName` from `settings.json` file.

### **3. BROWSER**

The Browser class is a kind of facade for Selenium WebDriver which contains methods for working with browser window and directly with WebDriver (for example, navigate, maximize, etc.). Writing a test script starts by creating an instance of `Browser` - more on this below.

#### 3.1. PARALLEL RUNS

The solution assumes the existence of only one instance of `Browser` class (has a property of `RemoteWebDriver` type) in the executing thread. Usually tests work with only one instance of browser and this approach is optimal.

If you are working on the task when more than one instance of browser per test is necessary, each browser has to be created in a separate thread. You can find an example of multi-threading usage here: [BrowserConcurrencyTests.cs](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/BrowserConcurrencyTests.cs).

If you are using standard ways of multi-threading from such tools as NUnit, MSTest, etc., every new thread will work with its own instance of `Browser` class.

#### 3.2. BROWSER MANAGER

There are several ways how to initialize `Browser`. The main one is based on the usage of `BrowserManager` class which has static property `Browser`. Below are the options of `BrowserManager` usage.

If you need to get the browser with configuration from settings file you just have to do:

```csharp
var browser = BrowserManager.Browser;
```

The first call of `Browser` creates the necessary instance with WebDriver (it opens web browser window, if it is not headless mode). All the following calls in the current thread will work with this instance.

#### 3.3. BROWSER FACTORY

`BrowserManager` uses browser factory to create an instance of `Browser`. There are two implementations of browser factory in the solution:

- [LocalBrowserFactory](../Aquality.Selenium/src/Aquality.Selenium/Browsers/LocalBrowserFactory.cs) - for creating browser in case of `isRemote=false` parameter.
- [RemoteBrowserFactory](../Aquality.Selenium/src/Aquality.Selenium/Browsers/RemoteBrowserFactory.cs) - for creating browser in case of `isRemote=true` parameter.

Each factory implements `IBrowserFactory` interface which has only one property `Browser`. It allows you to create your own factory. If you want to use your own implementation of browser factory you have to set it in `BrowserManager` using method `SetFactory(IBrowserFactory browserFactory)` before the very first call of `Browser` property. The examples with custom factory can be found in [CustomBrowserFactoryTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomBrowserFactoryTests.cs) class.

If you don't want to use factories you have an option to create an instance of `Browser` by yourself and set it as value of `BrowserManager.Browser` property. `Browser` class has a public constructor `public Browser(RemoteWebDriver webDriver, IConfiguration configuration)`. You can still use existing implementations of `IDriverSettings`, `ITimeoutConfiguration`, ect., during the creation of your own `IConfiguration`.

#### 3.4. DRIVER OPTIONS

Implementation of `IDriverSettings` is using during the creation process of `Browser` and in particular WebDriver. `IDriverSettings` includes property `DriverOptions` which is set to WebDriver during its instantiating. If you use default `BrowserFactory`, the list of options will be created based on the information from [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file.

You can find the example with user options here: [Should_BePossibleToUse_CustomFactory](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomBrowserFactoryTests.cs).

#### 3.5. DOWNLOAD DIRECTORY

It is often necessary to download files from browser and then perform some actions with them. To get the current download directory you can use property `DownloadDirectory` of `Browser` class.

To support this functionality `IDriverSettings` interface obliges to implement property `string DownloadDir { get; }`. You if use one of the existing `BrowserFactory` you can set download directory in [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file. For example:

```json
{
  "download.default_directory": "//home//selenium//downloads"
}
```

Please note that key `download.default_directory` is differ for different browser. You can get the name of this key in appropriate classes:

- [ChromeSettings.cs](../Aquality.Selenium/src/Aquality.Selenium/Configurations/WebDriverSettings/ChromeSettings.cs)
- [FirefoxSettings.cs](../Aquality.Selenium/src/Aquality.Selenium/Configurations/WebDriverSettings/FirefoxSettings.cs)
- [SafariSettings.cs](../Aquality.Selenium/src/Aquality.Selenium/Configurations/WebDriverSettings/SafariSettings.cs)

At the moment the library supports file downloading only in Chrome, Firefox, Safari browsers.

#### 3.6. ALERTS

`Browser` class provides methods to work with Alerts:

```csharp
BrowserManager.Browser.HandleAlert(AlertAction.Accept);
```

You can find more examples in [AlertTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/AlertTests.cs).

#### 3.7. SCREENSHOTS

`Browser` class has the method to get screenshot of web page:

```csharp
var screenshot = BrowserManager.Browser.GetScreenshot();
```

For details please see the following test: [Should_BePossibleTo_TakeScreenshot](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/BrowserTests.cs).

### **4. ELEMENTS**

When `Browser` is initialized and desired web page is opened you can start to work with its elements.

#### 4.1. ELEMENT FACTORY

There is an [ElementFactory](../Aquality.Selenium/src/Aquality.Selenium/Elements/ElementFactory.cs) class in the library which is responsible for creating elements of desired type. Below is the example of getting `ITextBox` element:

```csharp
var elementFactory = new ElementFactory();
var usernameTextBox = elementFactory.GetTextBox(By.Id("username"), "Username");
```

Using `ElementFactory` you can create an instance of any class that implements `IElement` interface. There are a set of methods in `ElementFactory` which returns implementations of `IElement` that library has (`IButton`, `ITextBox`, `ICheckBox`, etc.). Please use interfaces as much as possible.

#### 4.2. CUSTOM ELEMENTS

The user is able to create his own element or extend the existing one. `ElementFactory` provides method `T GetCustomElement<T>` for this purpose. You need just to implement `IElement` interface or extend the class of existing element. An example of extension and use can be found  in [CustomElementTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomElementTests.cs) class.

#### 4.3. LIST OF ELEMENTS

`ElementFactory` provides method `FindElements` to get the list of desired elements, the usage of which is demonstrated below:

```csharp
var checkBoxes = elementFactory.FindElements<ICheckBox>(By.XPath("//*[@class='checkbox']"));
```

You can find other examples with `ElementFactory` and elements in [Element Tests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Elements).


#### 4.4. STATES OF ELEMENTS

Depending on the task you may need to find only Displayed elements on the page or elements which at least exist in the source html (ExistsInAnyState). 

To get such elements and work with them methods from `ElementFactory` have optional parameter `state`. For example:

```csharp
var link = elementFactory.GetLink(By.Id("redirect"), "Link", state: ElementState.Displayed);
```

The often situation during the work with elements is to check element state or waiting for desired element state. [ElementStateProvider](../Aquality.Selenium/src/Aquality.Selenium/Elements/ElementStateProvider.cs) class helps to do this. Element has `State` property which provides an access to the instance of this class:

```csharp
UserNameTextBox.State.WaitForEnabled();
var isDisplayed = UserNameTextBox.State.IsDisplayed;
```

You can get more examples in [ElementStateTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/ElementStateTests.cs) class.

### **5. FORMS**

The main goal of this library is to help with test automation of web applications. There is a popular practice using [Page Objects](https://github.com/SeleniumHQ/selenium/wiki/PageObjects) approach in test automation. To support and extend this approach solution provides [Form](../Aquality.Selenium/src/Aquality.Selenium/Forms/Form.cs) class which can be used as a parent for all pages and forms of the application under test. Example of usage: 

```csharp
public class SliderForm : Form 
{
    public SliderForm() : base(By.Id("slider_row"), "Slider") 
    {
    }
}
```

`Id = "slider_row"` is a locator which will be used when checking the opening of the page/form using `IsDisplayed` property from [Form](../Aquality.Selenium/src/Aquality.Selenium/Forms/Form.cs) class.

Example of test with Page Objects: [ShoppingCartTest](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/ShoppingCartTest.cs).

### **6. JAVASCRIPT EXECUTION**

If you need to execute JavaScript on opened web page you can use one of `ExecuteScript` methods from `Browser` class.
The solution contains a sufficient amount of most popular JS scripts which are using in test automation. The list of available scripts is represented by [JavaScript](../Aquality.Selenium/src/Aquality.Selenium/Browsers/JavaScript.cs) enum. The scripts are located in resource directory [JavaScripts](../Aquality.Selenium/src/Aquality.Selenium/Resources/JavaScripts).
The examples of methods usages are defined in [BrowserTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/BrowserTests.cs) class.

There are also an overridden methods to pass JavaScript directly from file or as string in `Browser` class.

### **7. JSON FILE**

Aquality Selenium uses class [JsonFile](../Aquality.Selenium/src/Aquality.Selenium/Utilities/JsonFile.cs) and provides access to it.
This class provides useful methods to work with JSON files from your project.
For example, if you want to store web application URL which you are working with as a parameter in configuration, you can get its value from JSON like this:

```csharp
var environment = new JsonFile("settings.json");
var url = environment.GetValue<string>(".url");
```
