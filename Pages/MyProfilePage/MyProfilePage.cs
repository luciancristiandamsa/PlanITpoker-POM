using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PlanITpokerPOM.Components;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PlanITpokerPOM
{
    public class MyProfilePage : FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public MyProfilePage(IWebDriver driver) 
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='img-responsive img-circle']")));
        }

        public void UploadAndSetTheProfileImage(string pathToImageLocation)
        {
            Thread.Sleep(2000);
            ProfileImageUpload.Click();
            Thread.Sleep(2000);
            SendKeys.SendWait(pathToImageLocation);
            SendKeys.SendWait("{Enter}");
        }

        private void TextBoxName(string name)
        {
            NameTextBox.Clear();
            NameTextBox.SendKeys(name);
        }

        private void TextBoxEmail(string email)
        {
            EmailTextBox.Clear();
            EmailTextBox.SendKeys(email);
        }

        private void TextBoxPassword(string password)
        {
            PasswordTextBox.Clear();
            PasswordTextBox.SendKeys(password);
        }

        public MyProfilePage FillOutTheFormAndUpdate(string name, string email, string password)
        {
            TextBoxName(name);
            TextBoxEmail(email);
            TextBoxPassword(password);
            UpdateButton.Submit();
            return new MyProfilePage(driver);
        }

        public bool CheckWhetherProfileImageWasSuccessfullyUpdated()
        {
           IWebElement webElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("fileUpload")));
            return webElement.Displayed;
        }

        public bool GetTheUrlOfThisPage => driver.Url.Contains("profile");
        private IWebElement ProfileImageUpload => driver.FindElement(By.ClassName("fileUpload"));
        private IWebElement NameTextBox => driver.FindElement(By.Id("fullnameInput"));
        private IWebElement EmailTextBox => driver.FindElement(By.Id("emailInput"));
        private IWebElement PasswordTextBox => driver.FindElement(By.Id("passwordInput"));
        private IWebElement UpdateButton => driver.FindElements(By.XPath("//button[@type='submit']"))[0];
        private IWebElement GoogleLink => driver.FindElement(By.XPath("//*[contains(@ng-submit-form-value,'Google')]"));
    }
}