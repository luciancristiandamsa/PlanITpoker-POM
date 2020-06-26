using OpenQA.Selenium;
using PlanITpokerPOM.Components;

namespace PlanITpokerPOM
{
    public class HomePage : FooterPageElementsAndLogo
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LoginPage ClickOnTheLoginButton()
        {
            ClickLoginButton.Click();
            return new LoginPage(driver);
        }

        public SignUpPage ClickOnTheSignUpButton()
        {
            SignUpButton.Click();
            return new SignUpPage(driver);
        }

        public SignUpPage ClickOnTheSignUpNowButton()
        {
            SignUpButton.Click();
            return new SignUpPage(driver);
        }

        public StartAquickPlayPage ClickOnTheStartAquickPlayButton()
        {
            StartAquickPlayButoon.Click();
            return new StartAquickPlayPage(driver);
        }

        private IWebElement ClickLoginButton => driver.FindElement(By.XPath("//a[.='Login']"));

        public string GetTitlePage => driver.Title;  //driver.FindElement(By.XPath("//*[title='PlanITpoker: Online Scrum planning poker for Agile project teams']")).Text;

        private IWebElement SignUpButton => driver.FindElement(By.XPath("//a[.='Sign Up']"));

        private IWebElement SignUpNow => driver.FindElement(By.XPath("//a[.='Sign Up Now']"));

        private IWebElement StartAquickPlayButoon => driver.FindElement(By.XPath("//a[.='Start a quick play']"));
    }
}