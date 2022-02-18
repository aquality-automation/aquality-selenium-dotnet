using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Wrap over implementation of Selenium WebDriver IJavaScriptEngine.
    /// </summary>
    public class JavaScriptHandling : IJavaScriptEngine
    {
        private readonly IJavaScriptEngine javaScriptEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptHandling"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> instance on which the JavaScript events should be monitored.</param>
        public JavaScriptHandling(IWebDriver driver)
        {
            javaScriptEngine = new JavaScriptEngine(driver);
        }

        private ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Gets the read-only list of initialization scripts added for this JavaScript engine.
        /// </summary>
        public IReadOnlyList<InitializationScript> InitializationScripts
        {
            get
            {
                Logger.Info("loc.browser.javascript.initializationscripts.get");
                return javaScriptEngine.InitializationScripts;
            }
        }

        /// <summary>
        /// Gets the read-only list of binding callbacks added for this JavaScript engine.
        /// </summary>
        public IReadOnlyList<string> ScriptCallbackBindings
        {
            get
            {
                Logger.Info("loc.browser.javascript.scriptcallbackbindings.get");
                return javaScriptEngine.ScriptCallbackBindings;
            }
        }

        /// <summary>
        /// Occurs when a JavaScript callback with a named binding is executed.
        /// </summary>
        public event EventHandler<JavaScriptCallbackExecutedEventArgs> JavaScriptCallbackExecuted
        {
            add
            {
                Logger.Info("loc.browser.javascript.event.callbackexecuted.add");
                javaScriptEngine.JavaScriptCallbackExecuted += value;
            }
            remove
            {
                Logger.Info("loc.browser.javascript.event.callbackexecuted.remove");
                javaScriptEngine.JavaScriptCallbackExecuted -= value;
            }
        }

        /// <summary>
        /// Occurs when an exception is thrown by JavaScript being executed in the browser.
        /// </summary>
        public event EventHandler<JavaScriptExceptionThrownEventArgs> JavaScriptExceptionThrown
        {
            add
            {
                Logger.Info("loc.browser.javascript.event.exceptionthrown.add");
                javaScriptEngine.JavaScriptExceptionThrown += value;
            }
            remove
            {
                Logger.Info("loc.browser.javascript.event.exceptionthrown.remove");
                javaScriptEngine.JavaScriptExceptionThrown -= value;
            }
        }

        /// <summary>
        /// Occurs when methods on the JavaScript console are called. 
        /// </summary>
        public event EventHandler<JavaScriptConsoleApiCalledEventArgs> JavaScriptConsoleApiCalled
        {
            add
            {
                Logger.Info("loc.browser.javascript.event.consoleapicalled.add");
                javaScriptEngine.JavaScriptConsoleApiCalled += value;
            }
            remove
            {
                Logger.Info("loc.browser.javascript.event.consoleapicalled.remove");
                javaScriptEngine.JavaScriptConsoleApiCalled -= value;
            }
        }

        /// <summary>
        /// Occurs when a value of an attribute in an element is being changed.
        /// </summary>
        public event EventHandler<DomMutatedEventArgs> DomMutated
        {
            add
            {
                Logger.Info("loc.browser.javascript.event.dommutated.add");
                javaScriptEngine.DomMutated += value;
            }
            remove
            {
                Logger.Info("loc.browser.javascript.event.dommutated.remove");
                javaScriptEngine.DomMutated -= value;
            }
        }

        /// <summary>
        /// Asynchronously adds JavaScript to be loaded on every document load.
        /// </summary>
        /// <param name="scriptName">The friendly name by which to refer to this initialization script.</param>
        /// <param name="script">The JavaScript to be loaded on every page.</param>
        /// <returns>A task containing an <see cref="InitializationScript"/> object representing the script to be loaded on each page.</returns>
        public async Task<InitializationScript> AddInitializationScript(string scriptName, string script)
        {
            Logger.Info("loc.browser.javascript.initializationscript.add", scriptName);
            return await javaScriptEngine.AddInitializationScript(scriptName, script);
        }

        /// <summary>
        /// Asynchronously removes JavaScript from being loaded on every document load.
        /// </summary>
        /// <param name="scriptName">The friendly name of the initialization script to be removed.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task RemoveInitializationScript(string scriptName)
        {
            Logger.Info("loc.browser.javascript.initializationscript.remove", scriptName);
            await javaScriptEngine.RemoveInitializationScript(scriptName);
        }

        /// <summary>
        /// Asynchronously removes all initialization scripts from being
        /// loaded on every document load.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ClearInitializationScripts()
        {
            Logger.Info("loc.browser.javascript.initializationscripts.clear");
            await javaScriptEngine.ClearInitializationScripts();
        }

        /// <summary>
        /// Asynchronously adds a binding to a callback method that will raise
        /// an event when the named binding is called by JavaScript executing
        /// in the browser.
        /// </summary>
        /// <param name="bindingName">The name of the callback that will trigger events when called by JavaScript executing in the browser.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddScriptCallbackBinding(string bindingName)
        {
            Logger.Info("loc.browser.javascript.scriptcallbackbinding.add", bindingName);
            await javaScriptEngine.AddScriptCallbackBinding(bindingName);
        }

        /// <summary>
        /// Asynchronously removes a binding to a JavaScript callback.
        /// </summary>
        /// <param name="bindingName">The name of the callback to be removed.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task RemoveScriptCallbackBinding(string bindingName)
        {
            Logger.Info("loc.browser.javascript.scriptcallbackbinding.remove", bindingName);
            await javaScriptEngine.RemoveScriptCallbackBinding(bindingName);
        }

        /// <summary>
        /// Asynchronously removes all bindings to JavaScript callbacks.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ClearScriptCallbackBindings()
        {
            Logger.Info("loc.browser.javascript.scriptcallbackbindings.clear");
            await javaScriptEngine.ClearScriptCallbackBindings();
        }

        /// <summary>
        /// Enables monitoring for DOM changes.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task EnableDomMutationMonitoring()
        {
            Logger.Info("loc.browser.javascript.dommutation.monitoring.enable");
            await javaScriptEngine.EnableDomMutationMonitoring();
        }

        /// <summary>
        /// Disables monitoring for DOM changes.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DisableDomMutationMonitoring()
        {
            Logger.Info("loc.browser.javascript.dommutation.monitoring.disable");
            await javaScriptEngine.DisableDomMutationMonitoring();
        }

        /// <summary>
        /// Pins a JavaScript snippet for execution in the browser without transmitting the
        /// entire script across the wire for every execution.
        /// </summary>
        /// <param name="script">The JavaScript to pin</param>
        /// <returns>A task containing a <see cref="PinnedScript"/> object to use to execute the script.</returns>
        public async Task<PinnedScript> PinScript(string script)
        {
            Logger.Info("loc.browser.javascript.snippet.pin");
            return await javaScriptEngine.PinScript(script);
        }

        /// <summary>
        /// Unpins a previously pinned script from the browser.
        /// </summary>
        /// <param name="script">The <see cref="PinnedScript"/> object to unpin.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UnpinScript(PinnedScript script)
        {
            Logger.Info("loc.browser.javascript.snippet.unpin");
            await javaScriptEngine.UnpinScript(script);
        }

        /// <summary>
        /// Asynchronously starts monitoring for events from the browser's JavaScript engine.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task StartEventMonitoring()
        {
            Logger.Info("loc.browser.javascript.event.monitoring.start");
            await javaScriptEngine.StartEventMonitoring();
        }

        /// <summary>
        /// Stops monitoring for events from the browser's JavaScript engine.
        /// </summary>
        public void StopEventMonitoring()
        {
            Logger.Info("loc.browser.javascript.event.monitoring.stop");
            javaScriptEngine.StopEventMonitoring();
        }

        /// <summary>
        /// Asynchronously removes all bindings to JavaScript callbacks and all
        /// initialization scripts from being loaded for each document.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ClearAll()
        {
            Logger.Info("loc.browser.javascript.clearall");
            await javaScriptEngine.ClearAll();
        }

        /// <summary>
        /// Asynchronously removes all bindings to JavaScript callbacks, all
        /// initialization scripts from being loaded for each document, and
        /// stops listening for events.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Reset()
        {
            Logger.Info("loc.browser.javascript.reset");
            await javaScriptEngine.Reset();
        }
    }
}
