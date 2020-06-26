using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace PlanITpokerPOM
{
    public class GuestLoginPage
    {
        private readonly IWebDriver driverSecond;
        private WebDriverWait wait;
        private string nameOfThePlayer;

        public GuestLoginPage(IWebDriver driverSecond)
        {
            this.driverSecond = driverSecond;
            wait = new WebDriverWait(driverSecond, TimeSpan.FromSeconds(10));
        }

        public GameTablePage FillOutTheNameOfTheSecondPlayerAndSubmit(string nameOfThePlayer)
        {
            this.nameOfThePlayer = nameOfThePlayer;
            Thread.Sleep(3000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("inputName")));
            IWebElement enterNameFiled = driverSecond.FindElement(By.Name("inputName"));
            enterNameFiled.SendKeys(nameOfThePlayer);
            IWebElement enterButton = driverSecond.FindElement(By.XPath("//button[@ng-bs-click='enter']"));
            enterButton.Click();
            return new GameTablePage(driverSecond);
        }
    }
}