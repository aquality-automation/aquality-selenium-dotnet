using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenCvSharp;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Aquality.Selenium.Images.Locators
{
    /// <summary>
    /// Locator to search elements by image.
    /// Takes screenshot and finds match using openCV.
    /// Then finds elements by coordinates using JavaScript.
    /// </summary>
    public class ByImage : By, IDisposable
    {
        private readonly Mat template;
        private readonly string description;

        /// <summary>
        /// Constructor accepting image file.
        /// </summary>
        /// <param name="file">Image file to locate element by.</param>
        public ByImage(FileInfo file)
        {
            description= file.Name;
            template = new Mat(file.FullName, ImreadModes.Unchanged);
        }

        /// <summary>
        /// Constructor accepting image bytes.
        /// </summary>
        /// <param name="bytes">Image bytes to locate element by.</param>
        public ByImage(byte[] bytes)
        {
            description = $"bytes[%d]";
            template = Mat.ImDecode(bytes, ImreadModes.Unchanged);
        }

        /// <summary>
        /// Threshold of image similarity.
        /// Should be a float between 0 and 1, where 1 means 100% match, and 0.5 means 50% match.
        /// </summary>
        public virtual float Threshold { get; set; } = 1 - AqualityServices.Get<IVisualizationConfiguration>().DefaultThreshold;

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)?.FirstOrDefault() 
                ?? throw new NoSuchElementException($"Cannot locate an element using {ToString()}");
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var source = GetScreenshot(context);
            var result = new Mat();

            Cv2.MatchTemplate(source, template, result, TemplateMatchModes.CCoeffNormed);

            Cv2.MinMaxLoc(result, out _, out var maxVal, out _, out var matchLocation);
            var matchCounter = Math.Abs((result.Width - template.Width + 1) * (result.Height - template.Height + 1));
            var matchLocations = new List<Point>();
            while (matchCounter > 0 && maxVal >= Threshold)
            {
                matchCounter--;
                matchLocations.Add(matchLocation);
                Cv2.Rectangle(result, new Rect(matchLocation.X, matchLocation.Y, template.Width, template.Height), Scalar.Black, -1);
                Cv2.MinMaxLoc(result, out _, out maxVal, out _, out matchLocation);
            }

            return matchLocations.Select(match => GetElementOnPoint(match, context)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a single element on point (find by center coordinates, then select closest to matchLocation).
        /// </summary>
        /// <param name="matchLocation">Location of the upper-left point of the element.</param>
        /// <param name="context">Search context.
        /// If the searchContext is Locatable (like WebElement), will adjust coordinates to be absolute coordinates.</param>
        /// <returns>The closest found element.</returns>
        protected virtual IWebElement GetElementOnPoint(Point matchLocation, ISearchContext context)
        {
            if (context is ILocatable locatable)
            {
                var point = locatable.Coordinates.LocationInDom;
                matchLocation = matchLocation.Add(new Point(point.X, point.Y));
            }
            var centerLocation = matchLocation.Add(new Point(template.Width / 2, template.Height / 2));

            var elements = AqualityServices.Browser.ExecuteScript<IList<IWebElement>>(JavaScript.GetElementsFromPoint, centerLocation.X, centerLocation.Y)
                .OrderBy(element => DistanceToPoint(matchLocation, element));
            return elements.First();
        }

        /// <summary>
        /// Calculates distance from element to matching point.
        /// </summary>
        /// <param name="matchLocation">Matching point.</param>
        /// <param name="element">Target element.</param>
        /// <returns>Distance in pixels.</returns>
        protected virtual double DistanceToPoint(Point matchLocation, IWebElement element)
        {
            var elementLocation = element.Location;
            return Math.Sqrt(Math.Pow(matchLocation.X - elementLocation.X, 2) + Math.Pow(matchLocation.Y - elementLocation.Y, 2));
        }

        /// <summary>
        /// Takes screenshot from searchContext if supported, or from browser.
        /// Performs screenshot scaling if devicePixelRatio != 1.
        /// </summary>
        /// <param name="context">Search context for element location.</param>
        /// <returns>Captured screenshot as Mat object.</returns>
        protected virtual Mat GetScreenshot(ISearchContext context)
        {
            var screenshotBytes = context is ITakesScreenshot
                ? (context as ITakesScreenshot).GetScreenshot().AsByteArray
                : AqualityServices.Browser.GetScreenshot();
            var isBrowserScreenshot = context is IWebDriver || !(context is ITakesScreenshot);
            var source = Mat.ImDecode(screenshotBytes, ImreadModes.Unchanged);
            var devicePixelRatio = AqualityServices.Browser.ExecuteScript<long>(JavaScript.GetDevicePixelRatio);
            if (devicePixelRatio != 1 && isBrowserScreenshot)
            {
                var scaledWidth = (int)(source.Width / devicePixelRatio);
                var scaledHeight = (int)(source.Height / devicePixelRatio);
                Cv2.Resize(source, source, new Size(scaledWidth, scaledHeight), interpolation: InterpolationFlags.Area);
            }

            return source;
        }

        public override string ToString()
        {
            return $"ByImage: {description}, size: {template.Size()}";
        }

        public override bool Equals(object obj)
        {
            ByImage by = obj as ByImage;
            return by != null && template.ToString().Equals(by.template?.ToString());
        }

        public override int GetHashCode()
        {
            return template.GetHashCode();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                template.Dispose();
            }
        }
    }
}
