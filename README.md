[![Build Status](https://dev.azure.com/aquality-automation/aquality-automation/_apis/build/status/aquality-automation.aquality-selenium-dotnet?branchName=master)](https://dev.azure.com/aquality-automation/aquality-automation/_build/latest?definitionId=1&branchName=master)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=aquality-automation_aquality-selenium-dotnet&metric=alert_status)](https://sonarcloud.io/dashboard?id=aquality-automation_aquality-selenium-dotnet)
[![NuGet](https://img.shields.io/nuget/v/Aquality.Selenium)](https://www.nuget.org/packages/Aquality.Selenium)

# Aquality Selenium for .NET

### Overview

This package is a library designed to simplify your work with Selenium WebDriver.

You've got to use this set of methods, related to most common actions performed with web elements.

Most of performed methods are logged using NLog, so you can easily see a history of performed actions in your log.

We use interfaces where is possible, so you can implement your own version of target interface with no need to rewrite other classes.

### Quick start
To start the project using Aquality.Selenium framework, you can [download our template BDD project by this link.](https://github.com/aquality-automation/aquality-selenium-dotnet-template)

Alternatively, you can follow the steps below:

1. Add the nuget dependency Aquality.Selenium to your project.

2. Create instance of Browser in your test method:
```csharp
var browser = AqualityServices.Browser;
```

3. Use Browser's methods directly for general actions, such as navigation, window resize, scrolling and alerts handling:
```csharp
browser.Maximize();
browser.GoTo("https://wikipedia.org");
browser.WaitForPageToLoad();
```

4. Use ElementFactory class's methods to get an instance of each element:
```csharp
var emailTextBox = AqualityServices.Get<IElementFactory>().GetTextBox(By.Id("email_create"), "Email");
```
Or you can inherit a class from Form class and use existing ElementFactory:
```csharp
private ITextBox EmailTextBox => ElementFactory.GetTextBox(By.Id("email_create"), "Email");
```

5. Call element's methods to perform action with element: 
```csharp
emailTextBox.Type("email@domain.com");
```

6. Handle basic authentication:
```csharp
Assert.DoesNotThrowAsync(() => browser.RegisterBasicAuthenticationAndStartMonitoring("domain.com", "username", "password"),
                "Should be possible to set basic authentication async");
```
or intercept network requests/responses:
```csharp
browser.Network.AddRequestHandler(
    new NetworkRequestHandler 
    { 
        RequestMatcher = req => true,
        ResponseSupplier = req => new HttpResponseData { Body = "my body content", StatusCode = 200 }
    });
Assert.DoesNotThrowAsync(() => browser.Network.StartMonitoring());
```
see more examples at [NetworkHandlingTests](Aquality.Selenium/tests/Aquality.Selenium.Tests/Integration/NetworkHandlingTests.cs).

7. Emulate GeoLocation, Device, Touch, Media, UserAgent overrides, Disable script execution and more using DevTools extensions:
```csharp
const double latitude = 35.8235;
const double longitude = -78.8256;
const double accuracy = 0.97;
Assert.DoesNotThrowAsync(() => DevTools.SetGeoLocationOverride(latitude, longitude, accuracy), "Should be possible to override geoLocation")
```

It is also possible to set mobile emulation capabilities (for chromium-based browsers) in Resources/settings.json file, as well as to configure other arguments and options there:
```json
{
  "browserName": "chrome",
  "isRemote": false,
  "remoteConnectionUrl": "http://qa-auto-nexus:4444/wd/hub",
  "isElementHighlightEnabled": true,

  "driverSettings": {
    "chrome": {
      "capabilities": {
        "selenoid:options": 
        { 
            "enableVNC": true 
        },
        "mobileEmulation": {
          "userAgent": "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 5 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19",
          "deviceMetrics": {
            "width": 660,
            "height": 1040,
            "pixelRatio": 3.0
          }
        },
        "unhandledPromptBehavior": "ignore"
      },
      "options": {
        "download.prompt_for_download": "false",
        "download.default_directory": "./downloads"
      },
      "loggingPreferences": {
          "Performance": "All"
      },
      "excludedArguments": [ "enable-automation" ],
      "startArguments": [ "--disable-search-engine-choice-screen" ],
      "pageLoadStrategy": "Normal"
    },
    "safari": {
      "options": {
        "safari.options.dataDir": "/Users/username/Downloads"
      }
    }
  },
  "timeouts": {
    "timeoutImplicit": 0,
    "timeoutCondition": 30,
    "timeoutScript": 10,
    "timeoutPageLoad": 60,
    "timeoutPollingInterval": 300,
    "timeoutCommand": 60
  },
  "retry": {
    "number": 2,
    "pollingInterval": 300
  },
  "logger": {
    "language": "en",
    "logPageSource": true
  },
  "elementCache": {
    "isEnabled": false
  },
  "visualization": {
    "defaultThreshold": 0.012,
    "comparisonWidth": 16,
    "comparisonHeight": 16,
    "pathToDumps": "../../../Resources/VisualDumps/"
  }
}
```

8. Quit browser at the end:
```csharp
browser.Quit();
```

### Documentation
To get more details please look at wiki:
- [In English](https://github.com/aquality-automation/aquality-selenium-dotnet/wiki/Overview-(English))
- [In Russian](https://github.com/aquality-automation/aquality-selenium-dotnet/wiki/Overview-(Russian))

### License
Library's source code is made available under the [Apache 2.0 license](LICENSE).
