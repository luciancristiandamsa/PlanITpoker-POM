using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PlanITpokerPOM.Pages.YourPokerRoomsPage;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;

namespace PlanITpokerPOM
{
    public class CreateNewRoomPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private string roomName;
        private CardType cardsType;
        private TimerValue timerValue;
        private bool timer;

        public CreateNewStoryPage InitalizeCreateNewRoom(string roomName, CardType cardsType, ArrayList arrayLists, bool timer, TimerValue timerValue)
        {
            this.roomName = roomName;
            this.cardsType = cardsType;
            this.timerValue = timerValue;
            this.timer = timer;

            EnterTheRoomName(roomName);
            SelectCardsType();
            UseMultipleCardNumbers(arrayLists);
            ReturnTheTimerValue(timerValue);
            CreateButton.Click();
            return new CreateNewStoryPage(driver);
        }

        public CreateNewStoryPage InitalizeCreateNewRoom(string roomName, CardType cardsType, ArrayList arrayLists)
        {
            EnterTheRoomName(roomName);
            SelectCardsType();
            UseMultipleCardNumbers(arrayLists);
            CreateButton.Click();
            return new CreateNewStoryPage(driver);
        }
        
        public CreateNewStoryPage InitalizeCreateNewRoom(string roomName, CardType cardsType)
        {
            EnterTheRoomName(roomName);
            SelectCardsType();
            CreateButton.Click();
            return new CreateNewStoryPage(driver);
        }
        
        public CreateNewStoryPage InitalizeCreateNewRoom(string roomName, CardType cardsType, bool timer, TimerValue timerValue)
        {
            EnterTheRoomName(roomName);
            SelectCardsType();
            ReturnTheTimerValue(timerValue);
            CreateButton.Click();
            return new CreateNewStoryPage(driver);
        }

        public CreateNewRoomPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("createRoomNameInput")));
        }

        private void EnterTheRoomName(string roomName)
        {
            Thread.Sleep(500);
            EnterRoomName.Clear();
            EnterRoomName.SendKeys(roomName);
        }

        private void SelectCardsType()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(CardType, "1"));
            SelectElement select = new SelectElement(CardType);
            switch (cardsType)
            {
                case Pages.YourPokerRoomsPage.CardType.Scrum:
                    select.SelectByValue("1");
                    break;
                case Pages.YourPokerRoomsPage.CardType.Fibonacci:
                    select.SelectByValue("2");
                    break;
                case Pages.YourPokerRoomsPage.CardType.Sequential:
                    select.SelectByValue("3");
                    break;
                case Pages.YourPokerRoomsPage.CardType.PlayingCards:
                    select.SelectByValue("4");
                    break;
                case Pages.YourPokerRoomsPage.CardType.TShirt:
                    select.SelectByValue("5");
                    break;
            }
        }

        private void DropdownCustomizeCardValues()
        {
            Thread.Sleep(777);
            IWebElement dropDownCustomizeCards = driver.FindElement(By.XPath("//*[.='Customize cards values']"));
            dropDownCustomizeCards.Click();
            Thread.Sleep(777);
        }

        private void ReturnTheTimerValue(TimerValue value)
        {
            DoYouWantToUseAcountdownTimer.Click();
            SelectElement timerVaules = new SelectElement(driver.FindElement(By.XPath("//*[@ng-model='countdownTimerValue']")));

            timerVaules.SelectByIndex((int)value);
        }

        private void UseMultipleCardNumbers(ArrayList cardName)
        {
            DropdownCustomizeCardValues();

            foreach (var item in cardName)
            {
                IWebElement chooseTheNumber = driver.FindElement(By.XPath($"//*[@ng-show='createForm.cardSetType == {(int)cardsType + 1}']//*[.='{item}']"));
                chooseTheNumber.Click();
            }
        }

        private void UseAllCardsValue()
        {
            DropdownCustomizeCardValues();
            if (!UseAllCards)
            {
                IWebElement useAllCards = driver.FindElement(By.XPath("//*[@ng-model='createForm.cardsCustomize.useAll']"));
                useAllCards.Click();
            }
        }

        public void MarkDoYouWantToEnterStoriesInThisRoom()
        {
            if (!DoYouWantToEnterStoriesInThisRoom.Selected)
            {
                DoYouWantToEnterStoriesInThisRoom.Click();
            }
        }

        public void MarkRequestConfirmationWhenSkippingStories()
        {
            Thread.Sleep(500);
            if (!RequestConfirmationWhenSkippingStories.Selected)
            {
                RequestConfirmationWhenSkippingStories.Click();
            }
        }

        public void MarkDoYouWantObserversToSeeOtherPlayersVotingInRealTime()
        {
            Thread.Sleep(500);
            if (!DoYouWantObserversToSeeOtherPlayersVotingInRealTime.Selected)
            {
                DoYouWantObserversToSeeOtherPlayersVotingInRealTime.Click();
            }
        }

        public void MarkDoYouWantToAutoRevealVotesWhenVotingCompleted()
        {
            Thread.Sleep(500);
            if (!DoYouWantToAutoRevealVotesWhenVotingCompleted.Selected)
            {
                DoYouWantToAutoRevealVotesWhenVotingCompleted.Click();
            }
        }

        public void MarkAllowPlayersToChangeVoteAfterScoresShown()
        {
            Thread.Sleep(500);
            if (!AllowPlayersToChangeVoteAfterScoresShown.Selected)
            {
                AllowPlayersToChangeVoteAfterScoresShown.Click();
            }
        }

        public YourPokerRoomsPage PressEscapeToReturnToYourPokerRoomPage()
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.Escape).Build().Perform();
            return new YourPokerRoomsPage(driver);
        }

        private IWebElement EnterRoomName => driver.FindElement(By.Id("createRoomNameInput"));
        private IWebElement CardType => driver.FindElement(By.XPath("//*[@ng-model='createForm.cardSetType']"));
        private IWebElement DoYouWantToEnterStoriesInThisRoom => driver.FindElement(By.XPath("(//*[@ng-model='createForm.haveStories'])[1]"));
        private IWebElement RequestConfirmationWhenSkippingStories => driver.FindElement(By.XPath("(//*[@ng-model='createForm.confirmSkip'])[1]"));
        private IWebElement DoYouWantObserversToSeeOtherPlayersVotingInRealTime => driver.FindElement(By.XPath("(//*[@ng-model='createForm.showVotingToObservers'])[1]"));
        private IWebElement DoYouWantToAutoRevealVotesWhenVotingCompleted => driver.FindElement(By.XPath("(//*[@ng-model='createForm.autoReveal'])[1]"));
        private IWebElement AllowPlayersToChangeVoteAfterScoresShown => driver.FindElement(By.XPath("(//*[@ng-model='createForm.changeVote'])[1]"));
        private IWebElement DoYouWantToUseAcountdownTimer => driver.FindElement(By.XPath("//*[.=' Do you want to use a countdown timer?']"));
        private IWebElement CreateButton => driver.FindElement(By.XPath("//*[.='Create']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//*[.='Cancel']"));
        private bool UseAllCards => driver.FindElement(By.XPath("//*[@ng-model='createForm.cardsCustomize.useAll']")).Selected;
        private ReadOnlyCollection<IWebElement> TimerValue => driver.FindElements(By.XPath("//*[@ng-show='countdownTimer']"));
    }
}