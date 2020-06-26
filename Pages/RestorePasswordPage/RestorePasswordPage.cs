using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PlanITpokerPOM
{
    public class RestorePasswordPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string emailToRestore;

        public RestorePasswordPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void EnterYourEmailAndRestoreThePassword(string emailToRestore)
        {
            this.emailToRestore = emailToRestore;
            EnterYourEmailTextField.Clear();
            EnterYourEmailTextField.SendKeys(emailToRestore);
            RestorePasswordButton.Click();
        }

        public bool CheckWhetherTheProcessWasSuccessful()
        {
            IWebElement message = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//*[.='Password has been sent to \"{emailToRestore}\" email. Check your mailbox for further actions.']")));
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("C://Users//Cristian Damsa//source//repos//PlanITpokerPOM//bin//Debug//net472//Screenshots//Login Tests//Restore Password Message.png", ScreenshotImageFormat.Png);
            return message.Displayed;
        }

        private IWebElement EnterYourEmailTextField => driver.FindElement(By.Name("inputEmail"));
        private IWebElement RestorePasswordButton => driver.FindElement(By.XPath("//*[@ng-bs-click='restore']"));
        private IWebElement ReturnToLoginHyperLink => driver.FindElement(By.XPath("//*[.='Return to login']"));
    }
}