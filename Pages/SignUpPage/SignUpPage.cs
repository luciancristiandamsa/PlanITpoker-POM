using OpenQA.Selenium;
using PlanITpokerPOM.Components;

namespace PlanITpokerPOM
{
    public class SignUpPage : FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;
        private string name;
        private string email;
        private string password;

        public CreateNewRoomPage InitalizeSignUpForm(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;

            FillOutNameField();
            FillOutEmailField();
            FillOutPasswordField();
            SignUpButton.Click();
            return new CreateNewRoomPage(driver);
        }

        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private void FillOutNameField()
        {
            EnterYourNameTextFiled.Clear();
            EnterYourNameTextFiled.SendKeys(name);
        }

        private void FillOutEmailField()
        {
            EnterYourEmailTextField.Clear();
            EnterYourEmailTextField.SendKeys(email);
        }

        private void FillOutPasswordField()
        {
            EnterYourPasswordTextField.Clear();
            EnterYourPasswordTextField.SendKeys(password);
        }

    
        private IWebElement EnterYourNameTextFiled => driver.FindElement(By.Name("inputName"));
        private IWebElement EnterYourEmailTextField => driver.FindElement(By.Name("inputEmail"));
        private IWebElement EnterYourPasswordTextField => driver.FindElement(By.Name("inputPassword"));
        private IWebElement SignUpButton => driver.FindElement(By.XPath("//*[.='Sign up']"));
        private IWebElement SignUpWithGoogleIcon => driver.FindElement(By.XPath("//*[@title='Google']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//*[.='Login']"));
    }
}