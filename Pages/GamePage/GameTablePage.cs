using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PlanITpokerPOM.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace PlanITpokerPOM
{
    public class GameTablePage : FooterPageElementsAndLogo
    {
        private readonly IWebDriver driver;
        private IWebDriver driverSecond;
        private readonly WebDriverWait wait;
        private string cardToPick;
        private string finalEstimate;
        public string[] cardsToPick;

        public void InitalizeGameTableWithoutSendingInvitations(string finalEstimate, params string[] cardsToPick)
        {
            this.cardsToPick = cardsToPick;
            this.finalEstimate = finalEstimate;

            StartButton.Click();
            ChooseTheCard();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//select[@ng-model='finalEstimate.value']")));
            ChooseFinalEstimate();
            FinishVotingButton.Click();
        }

        public void InitalizeGameTableWithSendingInvitations(string cardToPick, string finalEstimate, params string[] email)
        {
            this.cardToPick = cardToPick;
            this.finalEstimate = finalEstimate;

            SendInvitationsUsingEmailAddresses(email);
            StartButton.Click();
            ChooseTheCard();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//select[@ng-model='finalEstimate.value']")));
            ChooseFinalEstimate();
            FinishVotingButton.Click();
        }

        public void InitalizeGameTableWithoutSendingInvitationsAndChooseMultipleCards(string finalEstimate, params string[] cardsToPick)
        {
            this.cardsToPick = cardsToPick;
            this.finalEstimate = finalEstimate;

            StartButton.Click();
            ChooseTheCard();
            CheckWhetherTheCardIsSelectedCorrectly();
            ChooseFinalEstimate();
            FinishVotingButton.Click();
        }

        public GameTablePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='page-board-room']")));
        }
        
        public GameTablePage(IWebDriver driver, IWebDriver driverSecond)
        {
            this.driver = driver;
            this.driverSecond = driverSecond;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='page-board-room']")));
        }

        private void ChooseTheCard()
        {
            Thread.Sleep(2000);
            foreach (var item in cardsToPick)
            {
                IWebElement cardToChoose = driver.FindElement(By.XPath($"//div[@class='center-icon ng-binding' and .='{item}']"));
                cardToChoose.Click();
                Thread.Sleep(1000);
            }
        }

        private void ChooseFinalEstimate()
        {
            SelectElement chooseFinalEstimate = new SelectElement(driver.FindElement(By.XPath("//*[@ng-model='finalEstimate.value']")));
            chooseFinalEstimate.SelectByText(finalEstimate);
        }

        private void SendInvitationsUsingEmailAddresses(params string[] email)
        {
            foreach (var item in email)
            {
                EnterEmailsToSendInvitationsTextBox.SendKeys(item + ", ");
            }
            SendInvitationButton.Click();
        }

        public void TakeAShortNavigationThroughTheMainOptions()
        {
            Thread.Sleep(1000);
            TourGuideNextButton.Click();
            Thread.Sleep(1000);
            TourGuideNextButton.Click();
            Thread.Sleep(1000);
            TourGuideNextButton.Click();
            Thread.Sleep(1000);
            TourGuidEndTourButton.Click();
        }

        public string CheckWhetherTheCardIsSelectedCorrectly()
        {
            string el = driver.FindElement(By.XPath($"//*[@class='ng-binding ng-scope' and .='{cardsToPick[cardsToPick.Length - 1]}']")).Text;
            return el;
        }

        public bool ToastStoryEstimateWasSaved()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='toast-message' and .='Story estimate was saved.']")));
            return driver.FindElement(By.XPath("//*[@class='toast-message' and .='Story estimate was saved.']")).Displayed;
        }

        public bool ToastStoryWhenFileWasImportedSuccessfully()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(), 'imported successfully.')]")));
            return driver.FindElement(By.XPath("//div[contains(text(), 'imported successfully.')]")).Displayed;
        }

        public string CheckWhetherTheTimerWorksProperly()
        {
            return driver.FindElement(By.XPath("//span[@class='ng-binding' and contains(text(), '00:')]")).Text;
        }

        private string GetTheLinkToInviteTeammate()
        {
            InviteAteammateLink.Click();
            InviteAteammateLink.SendKeys(Keys.LeftControl + "c");
            ClickOnTheRoomName.Click();
            RoomNameTextField.Clear();
            RoomNameTextField.Click();
            RoomNameTextField.SendKeys(Keys.LeftControl + "v");
            CheckMarkRoomNameButton.Click();
            string link = ClickOnTheRoomName.Text;
            return link;
        }

        public GuestLoginPage SendTheLinkToAGuest()
        {
            driverSecond = new ChromeDriver();
            driverSecond.Navigate().GoToUrl(GetTheLinkToInviteTeammate());
            return new GuestLoginPage(driverSecond);
        }

        private ReadOnlyCollection<IWebElement> AllCrads => driver.FindElements(By.XPath("//div[@class='center-icon ng-binding' and .='1']"));
        private IWebElement CheckMarkRoomNameButton => driver.FindElement(By.XPath("//*[@class='btn btn-primary btn-sm editable-submit']"));
        private IWebElement CancelMarkRoomNameButton => driver.FindElement(By.XPath("//*[@class='btn btn-default btn-sm editable-cancel']"));
        private IWebElement ClickOnTheRoomName => driver.FindElement(By.XPath("//*[@ng-x-editable='updateGameTitle']"));
        private IWebElement RoomNameTextField => driver.FindElement(By.XPath("(//input[@type='text'])[1]"));
        private IWebElement CheckMarkStoryNameButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-sm editable-submit']"));
        private IWebElement CancelMarkStoryNameButton => driver.FindElement(By.XPath("//button[@class='btn btn-default btn-sm editable-cancel']"));
        private IWebElement StartButton => driver.FindElement(By.Id("btn-start"));
        private IWebElement DropdownInviteTeammate => driver.FindElement(By.Id("invite-members"));
        private IWebElement DropdownImageToChangeTheRole => driver.FindElement(By.XPath("//*[@class='img-border dropdown-toggle']"));
        private IWebElement ModeratorButton => driver.FindElement(By.XPath("//*[@class='moderator']"));
        private IWebElement PlayerButton => driver.FindElement(By.XPath("//*[.='Player ']"));
        private IWebElement ObserverButton => driver.FindElement(By.XPath("//*[.='Observer ']"));
        private IWebElement InviteAteammateLink => driver.FindElement(By.Id("invite-link"));
        private IWebElement FinishVotingButton => driver.FindElement(By.XPath("//*[@ng-bs-click='finish']"));
        private IWebElement ResetTimerButton => driver.FindElement(By.XPath("//*[@ng-bs-click='resetTimer']"));
        private IWebElement ClearVotesButton => driver.FindElement(By.XPath("//*[@ng-bs-click='clearAllVotes']"));
        private IWebElement SkipStoryButton => driver.FindElement(By.XPath("//*[@ng-bs-click='skip']"));
        private IWebElement NextStoryButton => driver.FindElement(By.XPath("//*[@ng-bs-click='next']"));
        private IWebElement EnterEmailsToSendInvitationsTextBox => driver.FindElement(By.Name("emailsInput"));
        private IWebElement SendInvitationButton => driver.FindElement(By.XPath("//*[@ng-bs-click='send']"));
        private IWebElement TourGuideNextButton => driver.FindElement(By.XPath("//*[.='Next ']"));
        private IWebElement TourGuidEndTourButton => driver.FindElement(By.XPath("//*[.='End tour']"));
        private IWebElement TourGuidPrevButton => driver.FindElement(By.XPath("//*[.=' Prev']"));
        private IWebElement FindOutMoreButton => driver.FindElement(By.XPath("//a[.='Find out more']"));

    }
}