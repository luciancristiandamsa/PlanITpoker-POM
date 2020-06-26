using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PlanITpokerPOM.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace PlanITpokerPOM
{
    public class YourPokerRoomsPage : FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private IAlert alert;

        public YourPokerRoomsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='row grayed-box-app']")));
        }

        private void ClickOnDropdown()
        {
            Thread.Sleep(1000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ProfileImgDropdown));
            ProfileImgDropdown.Click();
        }

        public MyProfilePage EnterInMyProfile()
        {
            ClickOnDropdown();
            MyProfileButton.Click();
            return new MyProfilePage(driver);
        }

        public YourPokerRoomsPage EnterInRooms()
        {
            ClickOnDropdown();
            RoomsButton.Click();
            return new YourPokerRoomsPage(driver);
        }

        public HomePage SignOutFromAccount()
        {
            ClickOnDropdown();
            SignOutButton.Click();
            return new HomePage(driver);
        }

        private void WaitUntilAlertIsPresent()
        {
            alert = driver.SwitchTo().Alert();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        private void AcceptAlert()
        {
            WaitUntilAlertIsPresent();
            alert.Accept();
        }

        private void DismissAlert()
        {
            WaitUntilAlertIsPresent();
            alert.Dismiss();
        }

        private int FindIndexFromRoom(string roomName)
        {
            int index = 0;
            foreach (var room in AllRooms)
            {
                if (room.Text.Contains(roomName))
                {
                    index = AllRooms.IndexOf(room);
                    break;
                }
                index++;
            }
            return index;
        }

        public YourPokerRoomsPage ResetACertainRoomByName(string roomName)
        {
            ResetRoomButton[FindIndexFromRoom(roomName)].Click();
            AcceptAlert();
            return new YourPokerRoomsPage(driver);
        }

        public CreateNewRoomPage EditACertainRoomByName(string roomName)
        {
            EditRoomButton[FindIndexFromRoom(roomName)].Click();
            return new CreateNewRoomPage(driver);
        }

        public YourPokerRoomsPage DeleteACertainRoomByName(string roomName)
        {
            DeleteRoomButton[FindIndexFromRoom(roomName)].Click();
            AcceptAlert();
            return new YourPokerRoomsPage(driver);
        }

        public CreateNewRoomPage ClickOnCreateNewRoom()
        {
            IWebElement createRoomElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[.='Create Room']")));
            createRoomElement.Click();
            return new CreateNewRoomPage(driver);
        }

        public bool ToastResetRoomSuccessfully()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'successfully reset')]")));
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(@"C:\Users\Cristian Damsa\source\repos\PlanITpokerPOM\bin\Debug\net472\Screenshots\Recent Rooms\Rest Room.png", ScreenshotImageFormat.Png);
            return driver.FindElement(By.XPath("//div[contains(text(), 'successfully reset')]")).Displayed;
        }
        
        public bool ToastEditRoomSuccessfully()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'Edit Room')]")));
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(@"C:\Users\Cristian Damsa\source\repos\PlanITpokerPOM\bin\Debug\net472\Screenshots\Recent Rooms\Edit Room.png", ScreenshotImageFormat.Png);
            return driver.FindElement(By.XPath("//div[contains(text(), 'Edit Room')]")).Displayed;
        }
        
        public int CheckTheNumberOfRoomsAfterDeleting()
        {
            Thread.Sleep(3000);
            return AllRooms.Count;
        }

        public int GetTheNumberOfAllRooms()
        {
            return AllRooms.Count;
        }

        public string GetTitlePages => driver.Title;
        public string GetUserName => driver.FindElement(By.ClassName("li-name")).Text;
        private IWebElement ProfileImgDropdown => driver.FindElement(By.XPath("//*[@id='profile-img']"));
        private IWebElement MyProfileButton => driver.FindElement(By.XPath("//*[.='My Profile']"));
        private IWebElement RoomsButton => driver.FindElement(By.XPath("//*[.='Rooms']"));
        private IWebElement SignOutButton => driver.FindElement(By.XPath("//*[.='Sign Out']"));
        private IWebElement CreateRoomButton => driver.FindElement(By.XPath("//*[.='Create Room']"));
        private ReadOnlyCollection<IWebElement> ResetRoomButton => driver.FindElements(By.XPath("//*[@class='reset-icon']"));
        private ReadOnlyCollection<IWebElement> EditRoomButton => driver.FindElements(By.XPath("//*[@class='edit-icon']"));
        private ReadOnlyCollection<IWebElement> DeleteRoomButton => driver.FindElements(By.XPath("//*[@class='delete-icon']"));
        private ReadOnlyCollection<IWebElement> AllRooms => driver.FindElements(By.XPath("//*[@ng-bind='game.title']"));
    }
}