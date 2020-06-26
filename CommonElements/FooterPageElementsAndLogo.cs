using OpenQA.Selenium;

namespace PlanITpokerPOM.Components
{
    public class FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;

        public PrivacyPolicyPage GoToPrivacyPolicyButton()
        {
            PrivacyPolicyButton.Click();
            return new PrivacyPolicyPage(driver);
        }

        public TermsAndConditionsyPage GoToTermsAndConditionsButton()
        {
            TermsAndConditionsButton.Click();
            return new TermsAndConditionsyPage(driver);
        }

        public HomePage ClickOnTheLogoPage()
        {
            LogoPage.Click();
            return new HomePage(driver);
        }

        private IWebElement PrivacyPolicyButton => driver.FindElement(By.XPath("//a[.='Privacy Policy']"));
        private IWebElement TermsAndConditionsButton => driver.FindElement(By.XPath("//a[.='Terms And Conditions']"));
        private IWebElement LogoPage => driver.FindElement(By.XPath("//*[@class='navbar-brand']"));
    }
}
