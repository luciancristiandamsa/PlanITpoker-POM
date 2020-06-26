using OpenQA.Selenium;
using System;

namespace PlanITpokerPOM
{
    public class CustomExpectedConditions
    {
        private readonly IWebDriver driver;
        public static Func<IWebDriver, bool> ElementDisplayed(IWebElement element)
        {
            bool condition(IWebDriver driver)
            {
                return element.Displayed;
            }
            return condition;
        }

        public static Func<IWebDriver, bool> ElementIsDisplayed(IWebElement element)
        {
            Func<IWebDriver, bool> myCustomConditions;

            myCustomConditions = driver =>
            {
                if (element != null)
                {
                    if (element.Displayed && element.Enabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            };
            return myCustomConditions;
           

        }


        
    }
}
