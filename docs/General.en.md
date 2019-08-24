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

Aquality Selenium provides flexible configuration to run tests by editing [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file.
Most of the settings are clear without further explanation but major points are highlighted below.
There is a possibility to implement your own configuration.

### 2.1. SETTINGS

Работа с решением подразумевает использование [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) или его измененных копий для запуска тестов.
По умолчанию используется файл [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) хранящийся в dll библиотеки как Embedded Resource.
Вы вам необходимо изменить какие-либо настройки в данном файле, вам необходимо создать такой же файл в директории `Resources` вашего проекта, к которому подключена данная библиотека, и уже в нем изменять необходимые параметы.
Также можно создать несколько копий `settings` файла для единовременного хранения нескольких конфигураций, отличающихся какими-либо параметрами.
При этом создавать данные файлы необходимо в той же директории `Resources`. 
Как правило это удобно, когда есть необходимость выполнять запуск тестов на различных операционных системах, машинах, браузерах и т.п.
Например, в настоящее время команда разработчиков Aquality Selenium использует две конфигурации [settings.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.json) и [settings.local.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.local.json) для выполнения запусков в локальном docker Circle CI и на персональной инфраструктуре.
Для того, чтобы удобно управлять тем, какой конфигурационный файл необходимо использовать можно установить переменную окружения с именем `profile` и присвоить ей желаемое значение (например, local).
По умолчанию во время запусков используется файл с именем `settings.json`.

Любой параметр [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) может быть также переопределен через переменную окружения.
Для этого необходимо указать `jsonPath` к параметру в JSON и его значение. Например:
`driverSettings.chrome.webDriverVersion: 77.0.3865.10`

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

Секция `retry` файла [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) отвечает за конфигурацию количества попыток выполнения операций над элементом.
Все операции над элементами (нажатия, ввод текста и т.п.) могут быть выполнены повторно в случае неудачи.
Данная логика заложена в классе [ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) посредством которого выполняются любые операции.
Параметр `number` означает количество предпринимаемых попыток выполнить операцию прежде чем выбросить исключение.
Параметр `pollingInterval` означает интервал между попытками в миллисекудах.
[ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) автоматически отлавливает исключения StaleElementReferenceException и InvalidElementStateException) и повторяет попытку снова. 

#### 2.6. LOGGING

The solution supports logging of operations (interaction with the browser, page elements). Logging Example:

`2019-07-18 10:14:08 INFO  - Label 'First product' :: Moving mouse to element`

Supported languages:

- [en](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/en.json) - English
- [ru](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/ru.json) - Russian

The value of logging language is set in the [logger.language](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) parameter.

#### 2.7. CLOUD USAGE

To set up the run on the remote server with Selenium Grid (Selenoid, Zalenium) or on such platforms as BrowserStack, Saucelabs, etc., it is require to set correct value of URL for connection to the service in `remoteConnectionUrl` parameter in [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) file. Also make sure that `isRemote` parameter has value **true**. For example, URL for BrowserStack can be the following [https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub](https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub).

#### 2.8. ACTIONS HIGHLIGHTING

`isElementHighlightEnabled` option is responsible for the need to highlight the elements of the web page with which the work is performed. Enabling the option allows you to more clearly observe the actions of the test.

#### 2.9. ACCESS FROM THE CODE

Доступ к данным из конфигурационного файла обеспечивается посредством обращения к методам экземпляра класса [Configuration](../Aquality.Selenium/src/Aquality.Selenium/Configurations/Configuration.cs)
Например:  
```csharp
var browserName = Configuration.Instance.BrowserProfile.BrowserName;
```
вернёт значение параметра "browser" из settings файла.

### **3. BROWSER**

The Browser class is a kind of facade for Selenium WebDriver which contains methods for working with browser window and directly with WebDriver (for example, navigate, maximize, etc.). Writing a test script starts by creating an instance of `Browser` - more on this below.

#### 3.1. PARALLEL RUNS

Решение предполагает наличие единственного экземпляра класса `Browser` (содержит поле типа `RemoteWebDriver`) в рамках одного потока исполнения. Как правило тесты работают с одним экземпляром браузера и данный подход оптимален.

Если вы решаете задачу использования в рамках одного теста несколько браузеров, каждый браузер необходимо создавать в отдельном потоке. С примерами работы в многопоточном режиме можно ознакомиться здесь [BrowserConcurrencyTests.cs](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/BrowserConcurrencyTests.cs)

Если вы используете стандартные средства параллелизации, предоставляемые такими инструментами как NUnit, MSTest и т.п., для каждого потока будет автоматически создан свой экземпляр `Browser`.

#### 3.2. BROWSER MANAGER

Существует несколько вариантов инициализации Browser. Основной способ базируется на использовании класса `BrowserManager` и его статического свойства `Browser`. Ниже рассматриваются варианты работы с `BrowserManager`.

Если нам необходимо получить браузер с данными из конфигурационого settings файла то достаточно просто произвести вызов:

```csharp
var browser = BrowserManager.Browser;
```

Первый вызов `Browser` создаст необходимый экземпляр с WebDriver (откроется окно браузера, если только не задан headless режим). Все последующие обращения в рамках одного потока будут работать с созданным экземпляром.

#### 3.3. BROWSER FACTORY

Неявно для пользователя `BrowserManager` предоставляет `Browser` посредством обращения к фабрике браузеров. В решение существуют следующие реализации фабрик:

- [LocalBrowserFactory](../Aquality.Selenium/src/Aquality.Selenium/Browsers/LocalBrowserFactory.cs) - для создания браузера в случае использования параметра `isRemote=false`
- [RemoteBrowserFactory](../Aquality.Selenium/src/Aquality.Selenium/Browsers/RemoteBrowserFactory.cs) - для создания браузера в случае использования параметра `isRemote=true`

Каждая реализация фабрики реализует интерфейс `IBrowserFactory` с единственным свойством `Browser`. Это предоставляет возможность реализовать свою фабрику.
Чтобы `Browser` возвращала `Browser`, созданный вашей реализацией фабрики, необходимо до первого вызова `Browser` установить в `BrowserManager` свою реализацию фабрики, 
используя метод `SetFactory(IBrowserFactory browserFactory)`. 
Примеры использования собственной фабрики можно рассмотреть здесь [CustomBrowserFactoryTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomBrowserFactoryTests.cs)

Если по каким либо причинам вы решите отказаться от использования фабрик, у вас остается возможность создать экземпляр `Browser` самостоятельно и в дальнейшем установить его в `BrowserManager.Browser`. 
Класс `Browser` содержит публичный конструктор со следующей сигнатурой `public Browser(RemoteWebDriver webDriver, IConfiguration configuration)`.
При этом, для создания собсвенной реализации `IConfiguration`, вам по-прежнему доступно использование уже имеющиxся реализаций `IDriverSettings`, `ITimeoutConfiguration` и т.д.

#### 3.4. DRIVER OPTIONS

В процессе создания `Browser` и в частности WebDriver используются реализации интерфейса `IDriverSettings`. Реализация включает свойство `DriverOptions`, которые впоследствии устанавливаются в WebDriver при его инициализации. 
Если вы пользуетесь `BrowserFactory` по умолчанию, список options будет сформирован на основании информации в [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) файле.

Пример с использованием пользовательских options представлен зедсь [Should_BePossibleToUse_CustomFactory](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomBrowserFactoryTests.cs).


#### 3.5. DOWNLOAD DIRECTORY

Не редким случаем является необходимость скачивать файлы в браузере и впоследствии производить с ними работу. Чтобы получить текущую директорию для загрузки можно воспользоваться свойством `DownloadDirectory` экземпляра `Browser`.

Для поддержания этой функциональности интерфейс `IDriverSettings` обязывает реализовать свойство `string DownloadDir { get; }`. Если вы используете одну из уже реализованных `BrowserFactory`, то директорию для загрузки файлов необходимо указать в файле [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json).
Например: 

```json
{
  "download.default_directory": "//home//selenium//downloads"
}
```

Обратите внимание, что ключ `download.default_directory` отличается для разных браузеров. Изучить какие ключи актуальны для каких браузеров можно в соответствующих  классах:

[ChromeSettings.cs](../Aquality.Selenium/src/Aquality.Selenium/Configurations/WebDriverSettings/ChromeSettings.cs)

[FirefoxSettings.cs](../Aquality.Selenium/src/Aquality.Selenium/Configurations/WebDriverSettings/FirefoxSettings.cs)

В настоящее время решение поддерживает загрузку файлов только в браузерах Chrome, Firefox, Safari.

#### 3.6. ALERTS

Класс `Browser` предоставляет методы работы с Alerts:

```csharp
BrowserManager.Browser.HandleAlert(AlertAction.Accept);
```

Больше примеров использования можно найти здесь [AlertTests.cs](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/AlertTests.cs).

#### 3.7. SCREENSHOTS

Для получения снимков экрана класс Browser предоставляет метод 

```csharp
var screenshot = BrowserManager.Browser.GetScreenshot();
```

Более подробный пример использования смотрите в тесте [Should_BePossibleTo_TakeScreenshot](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/BrowserTests.cs)

### **4. ELEMENTS**

Когда `Browser` инициализирован и осуществлен переход на требуемую страницу можно начинать работу с элементами этой страницы.

#### 4.1. ELEMENT FACTORY

Решение включает класс [ElementFactory](../Aquality.Selenium/src/Aquality.Selenium/Elements/ElementFactory.cs), который отвечает за создание элемента необходимого типа. Ниже приводится пример получения ITextBox:

```csharp
var elementFactory = new ElementFactory();
var usernameTextBox = elementFactory.GetTextBox(By.Id("username"), "Username");
```

`ElementFactory` способна создавать объекты любых классов, реализующих интерфейс `IElement`.
`ElementFactory` содержит ряд методов, которые возвращают реализации `IElement`, имеющиеся по умолчанию в решении (`IButton`, `ITextBox`, `ICheckBox` и т.д.). Обратите внимание, что работа с элементами ведется через интерфейсы, чтобы пользователь обращал внимание только на функциональность, но не на реализацию.

#### 4.2. CUSTOM ELEMENTS

The user is able to create his own element or extend the existing one. `ElementFactory` provides method `T GetCustomElement<T>` for this purpose. You need just to implement `IElement` interface or extend the class of existing element. An example of extension and use can be found  in [CustomElementTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomElementTests.cs) class.

#### 4.3. LIST OF ELEMENTS

`ElementFactory` provides method `FindElements` to get the list of desired elements, the usage of which is demonstrated below:

```csharp
var checkBoxes = ElementFactory.FindElements<ICheckBox>(By.XPath("//*[@class='checkbox']"));
```

You can find other examples with `ElementFactory` and elements in [Element Tests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Elements).


#### 4.4. STATES OF ELEMENTS

При работе с элементами страницы в зависимости от задачи как правило ожидается либо только нахождение элемента который виден на странице (Displayed), либо который хотя бы присутствует в верстке (Exists in any state).

Для получения и последующей работы с данными типами элементов `ElementFactory` предоставляет перегруженные методы получения элементов. Например,

```csharp
var link = ElementFactory.GetLink(By.Id("redirect"), "Link", ElementState.Displayed);
```

При работе с элементами частой является ситуация проверки состояния элемента или ожидание желаемого состояния.
Данная функциональность реализуется посредством класса [ElementStateProvider](../Aquality.Selenium/src/Aquality.Selenium/Elements/ElementStateProvider.cs)
Доступ к экземпляру этого класса можно получить посредством свойства `State` у элемента:

```csharp
UserNameTextBox.State.WaitForEnabled();
var isDisplayed = UserNameTextBox.State.IsDisplayed;
```

Больше примеров можно увидеть в классе [ElementStateTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/ElementStateTests.cs).

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
