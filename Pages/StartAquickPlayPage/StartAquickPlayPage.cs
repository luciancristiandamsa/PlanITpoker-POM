using OpenQA.Selenium;

namespace PlanITpokerPOM
{
    public class StartAquickPlayPage
    {
        private readonly IWebDriver driver;
        private string name;

        public CreateNewRoomPage InitStartAquickPlay(string name)
        {
            this.name = name;

            FillOutNameField();
            EnterButton.Click();
            return new CreateNewRoomPage(driver);
        }

        public StartAquickPlayPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private void FillOutNameField()
        {
            EnterYourNameTextField.Clear();
            EnterYourNameTextField.SendKeys(name);
        }

        private IWebElement EnterYourNameTextField => driver.FindElement(By.Name("inputName"));
        private IWebElement EnterButton => driver.FindElement(By.XPath("//*[.='Enter']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//*[.='Login']"));
    }
}