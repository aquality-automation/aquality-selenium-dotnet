# Aquality Selenium for .NET

Aquality Selenium является надстройкой над инструментом работы с браузером посредством Selenium WebDriver. Работа с Selenium WebDriver требует определенных навыков и опыта. Aquality Selenium предлагает упрощенный, а главное, более безопасный и стабильный способ работы с Selenium WebDriver.

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
В настоящее время Aquality Selenium позволяет автоматизировать веб тесты для Chrome, Firefox, Safari, IExplorer и Edge. Также присутствуют возможности самостоятельно реализовать поддержку новых браузеров, которые поддерживаются Selenium (подробнее [здесь](https://www.seleniumhq.org/about/platforms.jsp)).
При этом запуск тестов может выполняться на любой операционной системе с установленным на ней .NET Core SDK 2.1 и выше.

### 2. CONFIGURATIONS

Aquality Selenium предоставляет пользователю гибкие возможности по конфигурации запусков путём редактирования конфигурационного файла [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json)
Большинство настроек понятны без дополнительных объяснений, но основные моменты обозначены ниже в данном разделе.
Также существует возможность использования Aquality Selenium реализовав собственные классы конфигурации.

### 2.1. SETTINGS

Работа с решением подразумевает использование [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) или его измененных копий для запуска тестов.
По умолчанию используется файл [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) хранящийся в dll библиотеки как Embedded Resource.
Если вам необходимо изменить какие-либо настройки в данном файле, вам необходимо создать такой же файл в директории `Resources` вашего проекта, к которому подключена данная библиотека, и уже в нем изменять необходимые параметы.
Также можно создать несколько копий `settings` файла для единовременного хранения нескольких конфигураций, отличающихся какими-либо параметрами.
При этом создавать данные файлы необходимо в той же директории `Resources`. 
Как правило это удобно, когда есть необходимость выполнять запуск тестов на различных операционных системах, машинах, браузерах и т.п.
Например, в настоящее время команда разработчиков Aquality Selenium использует две конфигурации [settings.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.json) и [settings.local.json](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Resources/settings.local.json) для выполнения запусков в локальном docker Circle CI и на персональной инфраструктуре.
Для того, чтобы удобно управлять тем, какой конфигурационный файл необходимо использовать можно установить переменную окружения с именем `profile` и присвоить ей желаемое значение (например, local).
По умолчанию во время запусков используется файл с именем `settings.json`.

Любой параметр [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) может быть также переопределен через переменную окружения.
Для этого необходимо указать `jsonPath` к параметру в JSON и его значение. Например:
`driverSettings.chrome.webDriverVersion: 77.0.3865.10`

Settings файл содержит несколько секций, назначение которых описывается ниже.

#### 2.2. BROWSER
`browserName` параметр определяет на каком браузере будет выполняться запуск. Например browser=chrome - означает, что запуск осуществиться в Google Chrome.

`isRemote` параметр определят будет ли запуск выполняться на той же машине, где выполняется .NET процесс или использовать удалённый сервер, указанный в параметре `remoteConnectionUrl`.

#### 2.3. DRIVER SETTINGS
`driverSettings` секция файла [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) предоставляет возможность устанавливать необходимые capabilities, options или start arguments для web driver.

Для получения допустимых аргументов и опций обратитесь к официальным источникам от разработчиков браузеров. Например, для chrome: [run-chromium-with-flags](https://www.chromium.org/developers/how-tos/run-chromium-with-flags)

Мы постарались [здесь](./IExplorer_Settings.md) описать особенности работы с IExplorer самостоятельно ввиду разрознености информации на этот счёт в интернете.

#### 2.4. TIMEOUTS

[settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) содержит секцию `timeouts`, которая включает в себя набор параметров, связанных с различного рода таймаутами, используемыми в решении.

Все параметры данной конфигурации используются для инициализации объекта класса [TimeoutConfiguration](../Aquality.Selenium/src/Aquality.Selenium/Configurations/TimeoutConfiguration.cs), доступного впоследствии путем обращения `Configuration.Instance.TimeoutConfiguration`.

Ниже приводится описание параметров из секции `timeouts` c их назначением:

- `timeoutImplicit` = 0 секунд - значение неявного ожидания web driver'а [Selenium Implicit Wait](https://www.seleniumhq.org/docs/04_webdriver_advanced.jsp#implicit-waits)
- `timeoutCondition` = 15 секунд - время ожидания событий в решении. К событиям относятся ожидание элементов или их состояния
- `timeoutScript` = 10 секунд - данное значение служит лимитом выполнения скриптов с использованием метода WebDriver **ExecuteAsyncScript**
- `timeoutPageLoad` = 30 секунд - время ожидания загрузки страницы
- `timeoutPollingInterval` = 300 миллисекунд - интервал опроса в при явных ожиданиях
- `timeoutCommand` = 60 секунд - максимальное время ожидания выполнения каждой команды, отправляемой web driver'у 

В рамках решения все ожидания элементов выполняются при помощи Excplicit Wait. 
Перед ожиданием элемента значение implicit wait будет установлено в ноль принудительно, независимо от того, что находится в конфигурации.
Использование двух типов ожиданий не рекомендовано, так как может приводить к некорректному поведению.

#### 2.5 RETRY POLICY

Секция `retry` файла [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) отвечает за конфигурацию количества попыток выполнения операций над элементом.
Все операции над элементами (нажатия, ввод текста и т.п.) могут быть выполнены повторно в случае неудачи.
Данная логика заложена в классе [ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) посредством которого выполняются любые операции.
Параметр `number` означает количество предпринимаемых попыток выполнить операцию прежде чем выбросить исключение.
Параметр `pollingInterval` означает интервал между попытками в миллисекудах.
[ElementActionRetrier](../Aquality.Selenium/src/Aquality.Selenium/Utilities/ElementActionRetrier.cs) автоматически отлавливает исключения StaleElementReferenceException и InvalidElementStateException) и повторяет попытку снова. 

#### 2.6. LOGGING

Решение поддерживает логирование выполняемых операций (взаимодействие с браузером, элементами страницы). Пример логирования:

`2019-07-18 10:14:08 INFO  - Label &#39;First product&#39; :: Moving mouse to element`

Логирование поддерживается на языках:

- [en](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/en.json) - Английский
- [ru](../Aquality.Selenium/src/Aquality.Selenium/Resources/Localization/ru.json) - Русский

Значение языка логирования устанавливается в параметре [logger.language](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json).

#### 2.7. CLOUD USAGE

Для того, чтобы настроить запуск на удалённом сервере Selenium Grid (Selenoid, Zalenium) или на платформах вроде BrowserStack, Saucelabs и т.д., необходимо в конфигурационном файле [settings.json](../Aquality.Selenium/src/Aquality.Selenium/Resources/settings.json) установить корректное значение URL для подключения к сервису в параметр `remoteConnectionUrl`, а также убедиться, что параметр `isRemote` равен **true**.
Например, для BrowserStack параметр может иметь вид [https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub](https://USERNAME:AUTOMATE_KEY@hub-cloud.browserstack.com/wd/hub).

#### 2.8. ACTIONS HIGHLIGHTING

`isElementHighlightEnabled` параметр отвечает за необходимость подсветки элементов веб страницы с которыми производится работа. Включение опции позволяет более явно наблюдать за действиями теста.

#### 2.9. ACCESS FROM THE CODE

Доступ к данным из конфигурационного файла обеспечивается посредством обращения к методам экземпляра класса [Configuration](../Aquality.Selenium/src/Aquality.Selenium/Configurations/Configuration.cs)
Например:  
```csharp
var browserName = Configuration.Instance.BrowserProfile.BrowserName;
```
вернёт значение параметра "browser" из settings файла.

### **3. BROWSER**

Класс Browser, являющийся своего рода фасадом для Selenium WebDriver и содержит методы работы с окном браузера и непосредственно с WebDriver (например, navigate, maximize window и т.д.). Написание скрипта начинается с создания экземпляра `Browser` - подробнее об этом ниже.

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

Пользователь имеет возможность создать свой элемент или расширить имеющийся по умолчанию. Для этих целей `ElementFactory` предоставляет метод `T GetCustomElement<T>`. Достаточно лишь реализовать `IElement` интерфейс или расширить имеющийся класс элемента. С примером расширения и последующего использования можно ознакомиться в классе [CustomElementTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/CustomElementTests.cs).

#### 4.3. LIST OF ELEMENTS

Для получения списка элементов `ElementFactory` предоставляет метод `FindElements`, использование которого демонстрируется ниже:

```csharp
var checkBoxes = ElementFactory.FindElements<ICheckBox>(By.XPath("//*[@class='checkbox']"));
```

С другими примерами работы с `ElementFactory` и элементами можно ознакомиться здесь [Element Tests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Elements).


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

Основное назначение решения  - помощь в автоматизации тестирования Web приложений. Существует практика автоматизации с использованием подхода [Page Objects](https://github.com/SeleniumHQ/selenium/wiki/PageObjects). Для поддержания и расширения данного подхода решение предлагает к использованию класс [Form](../Aquality.Selenium/src/Aquality.Selenium/Forms/Form.cs), который может служить родительским классом для всех описываемых страниц и форм приложения. Пример использования:

```csharp
public class SliderForm : Form 
{
    public SliderForm() : base(By.Id("slider_row"), "Slider") 
    {
    }
}
```

Здесь `Id = "slider_row"` устанавливает локатор, который будет использован при проверке открытия страницы/формы, используя свойство `IsDisplayed` класса [Form](../Aquality.Selenium/src/Aquality.Selenium/Forms/Form.cs).

Пример теста с использованием Page Objects здесь [ShoppingCartTest](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/Usecases/ShoppingCartTest.cs).

### **6. JAVASCRIPT EXECUTION**

При необходимости выполнить какой либо JavaScript на открытой странице можно воспользоваться одним из методов `ExecuteScript` класса `Browser`.
Решение содержит достаточное количество наиболее используемых скриптов при выполнении автоматизации тестирования. Список скриптов представлен перечислением JavaScript. Сами скрипты расположены в директории ресурсов [JavaScripts](../Aquality.Selenium/src/Aquality.Selenium/Resources/JavaScripts).
Примеры использования метода имеются в классе [BrowserTests](../Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/BrowserTests.cs).

Также существует перегрузка для передачи файла с JavaScript или непосредственно строки со скриптом.

### **7. JSON FILE**

Aquality Selenium использует для своей работы и предоставляет доступ к классу [JsonFile](../Aquality.Selenium/src/Aquality.Selenium/Utilities/JsonFile.cs).
Данный класс предоставляет удобные методы для работы с JSON файлами вашего проекта.
Например, если вы захотите хранить URL сайта с которым вы работаете как параметр конфигурации вы сможете считывать значения из JSON при помощи указанного класса:

```csharp
var environment = new JsonFile("settings.json");
var url = environment.GetValue<string>(".url");
```
