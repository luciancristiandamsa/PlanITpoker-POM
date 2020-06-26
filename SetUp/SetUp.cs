using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Drawing;

namespace PlanITpokerPOM
{
    public abstract class SetUp
    {
        private readonly IWebDriver driver;

        public static IWebDriver BrowserName(Browser browserName)
        {
            switch (browserName)
            {
                case Browser.Chrome:
                    return new ChromeDriver();
                case Browser.Firefox:
                    return new FirefoxDriver();
                case Browser.InternetExplorer:
                    return new InternetExplorerDriver();
                case Browser.Opera:
                    return null;
                case Browser.Safari:
                    return null;
                default:
                    throw new Exception($"{browserName} doesn't exist.");
            }
        }

        public static void WindowCustomize(IWebDriver driver, int width, int heigh)
        {
            driver.Manage().Window.Position = new Point(width, heigh);
        }

        public static void WindowMaximize(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        public static void Goto(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        
        // Function for element visibility 
        public static bool IsVisible(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (Exception)
            {
                throw new Exception("Cant't find the element.");
            }
        }

        // Function for element visibility 
        public static Func<IWebDriver, IWebElement> ElementIsVisible(IWebElement element)
        {
            return (driver) =>
             {
                 try
                 {
                     return element;
                 }
                 catch (Exception)
                 {
                     throw new Exception("Can't find the element.");
                 }
             };
        }


        public enum Browser
        {
            Chrome, Firefox, InternetExplorer, Opera, Safari
        }
    }
}
