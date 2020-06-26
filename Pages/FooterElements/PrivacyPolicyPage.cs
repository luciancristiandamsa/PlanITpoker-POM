using OpenQA.Selenium;

namespace PlanITpokerPOM
{
    public class PrivacyPolicyPage
    {
        private IWebDriver driver;

        public PrivacyPolicyPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetTitlePage => driver.Title;
    }
}