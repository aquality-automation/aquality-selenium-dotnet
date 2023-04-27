using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.IO;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class BrokenImagesForm : TheInternetForm
    {
        private readonly By imageLocator = new ByImage(new FileInfo(Path.Combine(AppContext.BaseDirectory, "Resources", "BrokenImage.png")));

        public BrokenImagesForm() : base(By.Id("content"), "Broken Images form")
        {
        }

        public ILabel LabelByImage => ElementFactory.GetLabel(imageLocator, "broken image");
        
        public IList<ILabel> LabelsByImage => ElementFactory.FindElements<ILabel>(imageLocator, "broken image");
        
        public IList<ILabel> ChildLabelsByImage => FormElement.FindChildElements<ILabel>(imageLocator, "broken image");

        protected override string UrlPart => "broken_images";
    }
}
