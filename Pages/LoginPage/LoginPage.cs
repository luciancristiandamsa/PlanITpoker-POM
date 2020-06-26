using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PlanITpokerPOM.Components;
using System;

namespace PlanITpokerPOM
{
    public class LoginPage : FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void FillOutEnterYourEmailTextBox(string yourEmail)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(EnterYourEmailTextBox));
            EnterYourEmailTextBox.Clear();
            EnterYourEmailTextBox.SendKeys(yourEmail);
        }

        private void FillOutEnterYourPasswordTextBox(string yourPassword)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(EnterYourPasswordTextBox));
            EnterYourPasswordTextBox.Clear();
            EnterYourPasswordTextBox.SendKeys(yourPassword);
        }

        public YourPokerRoomsPage FillOutTheFormAndSubmit(string yourEmail, string yourPassword)
        {

            FillOutEnterYourEmailTextBox(yourEmail);
            FillOutEnterYourPasswordTextBox(yourPassword);
            LoginButton.Click();
            return new YourPokerRoomsPage(driver);
        }

        public bool VerifyWhetherURLContainsGoogle()
        {
            LoginWithGoogleLogoButton.Click();
            return driver.Url.Contains("google");
        }

        public RestorePasswordPage ClickOnTheForgottenPaswword()
        {
            ForgottenPasswordHyperLink.Click();
            return new RestorePasswordPage(driver);
        }

        public bool VerifyWhetherURLContainsGoogleWord => driver.FindElement(By.XPath("//button[@value='Google']")).Displayed;
        public bool GetTheUrlOfThisPage => driver.Url.Contains("authentication");   //driver.FindElement(By.XPath("//*[title='PlanITpoker: Login']")).Text;
        private IWebElement EnterYourEmailTextBox => driver.FindElement(By.Name("inputEmail"));
        private IWebElement EnterYourPasswordTextBox => driver.FindElement(By.Name("inputPassword"));
        private IWebElement ForgottenPasswordHyperLink => driver.FindElement(By.XPath("//*[.='Forgotten password?']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//*[.='Login']"));
        private IWebElement LoginWithGoogleLogoButton => driver.FindElement(By.XPath("//button[@value='Google']"));
        private IWebElement SignUpHyperlink => driver.FindElement(By.XPath("//*[.='Sign Up']"));
    }
}