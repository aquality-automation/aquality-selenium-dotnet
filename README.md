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

1. To start work with this package, simply add the nuget dependency Aquality.Selenium to your project.

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

6. Quit browser at the end:
```csharp
browser.Quit();
```

### Documentation
To get more details please look at wiki:
- [In English](https://github.com/aquality-automation/aquality-selenium-dotnet/wiki/Overview-(English))
- [In Russian](https://github.com/aquality-automation/aquality-selenium-dotnet/wiki/Overview-(Russian))

### License
Library's source code is made available under the [Apache 2.0 license](https://github.com/aquality-automation/aquality-selenium-dotnet/blob/master/LICENSE).
