using OpenQA.Selenium;

namespace PlanITpokerPOM
{
    public class TermsAndConditionsyPage
    {
        private IWebDriver driver;

        public TermsAndConditionsyPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetTitlePage => driver.Title;
    }
}