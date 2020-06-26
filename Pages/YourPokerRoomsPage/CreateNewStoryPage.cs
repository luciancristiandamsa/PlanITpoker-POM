using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PlanITpokerPOM
{
    public class CreateNewStoryPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private string[] stories;
        private string thePathToCsvOrXmlFile;

        public GameTablePage InitCreateNewStory(params string[] stories)
        {
            this.stories = stories;

            AddStoriesInTextField();
            SaveAndCloseButton.Click();
            return new GameTablePage(driver);
        }

        public GameTablePage InitCreateNewStory(string thePathToCsvOrXmlFile)
        {
            this.thePathToCsvOrXmlFile = thePathToCsvOrXmlFile;

            UploadCsvAndXml();
            Thread.Sleep(2000);
            SendKeys.SendWait(thePathToCsvOrXmlFile);
            SendKeys.SendWait("{Enter}");
            return new GameTablePage(driver);
        }

        private string CheckFileType()
        {
            string fileExtension = "";
            int lenght = thePathToCsvOrXmlFile.Length - 1;

                while (thePathToCsvOrXmlFile[lenght] != '.')
                {
                fileExtension += thePathToCsvOrXmlFile[lenght];
                lenght--;
                }
            return fileExtension;
        }

        private GameTablePage UploadCsvAndXml()
        {
            WaitToDisplayPage();
            UploadButton.Click();
            switch (CheckFileType())
            {
                case "vsc":
                    UploadCsvButton.Click();
                    break;
                case "lmx":
                    UploadXmlButton.Click();
                    break;
                default:
                    throw new Exception("Not allowed this extension file.");
            }
            return new GameTablePage(driver);
        }

        public CreateNewStoryPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void WaitToDisplayPage()
        {
            Thread.Sleep(2000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(PutYourStoriesTextField));
        }

        private void AddStoriesInTextField()
        {
            WaitToDisplayPage();
            foreach (var item in stories)
            {
                PutYourStoriesTextField.SendKeys(item);
                PutYourStoriesTextField.SendKeys("\n");
            }
        }

        private CreateNewRoomPage SaveAndAddNewStories()
        {
            WaitToDisplayPage();
            AddStoriesInTextField();
            SaveAndAddNewButton.Click();
            return new CreateNewRoomPage(driver);
        }


        private IWebElement PutYourStoriesTextField => driver.FindElement(By.Name("inputName"));
        private IWebElement SaveAndAddNewButton => driver.FindElement(By.XPath("//*[.='Save & Add New']"));
        private IWebElement SaveAndCloseButton => driver.FindElement(By.XPath("//*[.='Save & Close']"));
        private IWebElement UploadButton => driver.FindElement(By.XPath("//*[.='Upload']"));
        private IWebElement UploadCsvButton => driver.FindElement(By.ClassName("upload-option"));
        private IWebElement UploadXmlButton => driver.FindElement(By.XPath("//*[.='Upload Jira XML']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//*[.='Cancel']"));
    }
}