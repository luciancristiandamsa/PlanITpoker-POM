using OpenQA.Selenium;
using System.Collections;
using System.Linq;
using System.Threading;
using Xunit;

namespace PlanITpokerPOM
{
    public class Tests 
    {
        private readonly IWebDriver Driver;
        private const string url = "https://www.planitpoker.com/";

        // Particular values to choose instead of using All cards
        ArrayList customizeCardsScrum = new ArrayList() { "0", "1", "5", "13" };
        ArrayList customizeCardsFibonacci = new ArrayList() { "1", "3", "55" };
        ArrayList customizeCardsSequential = new ArrayList() { "2", "6", "10", "?", "Coffee" };
        ArrayList customizeCardsPlayingCards = new ArrayList() { "Ace", "King", "?" };
        ArrayList customizeCardsTshirt = new ArrayList() { "S", "M" };
        public Tests()
        {
            Driver = SetUp.BrowserName(SetUp.Browser.Chrome);
            SetUp.WindowCustomize(Driver, 100, 100);
            SetUp.WindowMaximize(Driver);
            SetUp.Goto(Driver, url);
        }

        [Trait("Category", "Login Tests")]
        [Fact]
        public void LoginWithValidCredentials()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");

            Assert.Equal("aaa098873", yourPokerRoomPage.GetUserName);
            Driver.Dispose();
        }

        [Trait("Category", "Login Tests")]
        [Fact]
        public void LoginWithGoogleAccount()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();

            Assert.True(loginPage.VerifyWhetherURLContainsGoogle());
            Driver.Dispose();
        }
        
        [Trait("Category", "Login Tests")]
        [Fact]
        public void RestoreTheForgottenPaswword()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var resotrePasswordPage = loginPage.ClickOnTheForgottenPaswword();
            resotrePasswordPage.EnterYourEmailAndRestoreThePassword("test@test.com");

            Assert.True(resotrePasswordPage.CheckWhetherTheProcessWasSuccessful());
            Driver.Dispose();
        }

        [Trait("Category", "SignUp Tests")]
        [Fact]
        public void SignUpWithValidCredentials()
        {
            var homePage = new HomePage(Driver);
            var signUpPage = homePage.ClickOnTheSignUpButton();
            var createNewRoomPage = signUpPage.InitalizeSignUpForm("aaa", "aaa@test.com", "123456qwerty");
            var yourPokerRoomPage = createNewRoomPage.PressEscapeToReturnToYourPokerRoomPage();

            Assert.Equal("aaa", yourPokerRoomPage.GetUserName);
            Driver.Dispose();
        }

        [Trait("Category", "SignUp Tests")]
        [Fact]
        public void SignUpNowWithValidCredentials()
        {
            var homePage = new HomePage(Driver);
            var signUpPage = homePage.ClickOnTheSignUpNowButton();
            var createNewRoomPage = signUpPage.InitalizeSignUpForm("aaa", "aaa@test.com", "123456qwerty");
            var yourPokerRoomPage = createNewRoomPage.PressEscapeToReturnToYourPokerRoomPage();

            Assert.Equal("aaa", yourPokerRoomPage.GetUserName);
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingAllCards()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitations("2", "8");

            Assert.True(gameTablePage.ToastStoryEstimateWasSaved());
            Driver.Dispose();
        }
        
        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingAllCardsAndTimer()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum, true, TimerValue.FiveMinutes);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();

            Assert.Equal("00:05:00", gameTablePage.CheckWhetherTheTimerWorksProperly());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingFourDifferentCards()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum, customizeCardsScrum);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitations("1", "5");

            Assert.True(gameTablePage.ToastStoryEstimateWasSaved());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingFourDifferentCardsAndTimer()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum, customizeCardsScrum,true, TimerValue.FourMinutes );
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();

            Assert.Equal("00:04:00", gameTablePage.CheckWhetherTheTimerWorksProperly());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingAllCardsAndAllowPlayersToChangeVoteAfterScoresShown()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            createNewRoomPage.MarkAllowPlayersToChangeVoteAfterScoresShown();
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitationsAndChooseMultipleCards("2", "8", "1", "0", "13");

            Assert.Equal("13", gameTablePage.CheckWhetherTheCardIsSelectedCorrectly());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Scrum Mode")]
        [Fact]
        public void StartAquickPlayInScrumModeUsingAllCardsAndUploadCSVfile()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum);
            var gameTablePage = createNewStoryPage.InitCreateNewStory(@"C:\Junior Resources\elements to upload\CSV File.csv");

            Assert.True(gameTablePage.ToastStoryWhenFileWasImportedSuccessfully());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingAllCards()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitations("2", "3");

            Assert.True(gameTablePage.ToastStoryEstimateWasSaved());
            Driver.Dispose();
        }
        
        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingAllCardsAndTimer()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential, true, TimerValue.ThreeMinutes);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();

            Assert.Equal("00:03:00", gameTablePage.CheckWhetherTheTimerWorksProperly());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingFourDifferentCards()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential, customizeCardsScrum);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitations("1", "13");

            Assert.True(gameTablePage.ToastStoryEstimateWasSaved());
            Driver.Dispose();
        }
        
        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingFourDifferentCardsAndTimer()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential, customizeCardsScrum,true, TimerValue.FourMinutes );
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();

            Assert.Equal("00:04:00", gameTablePage.CheckWhetherTheTimerWorksProperly());
            Driver.Dispose();
        }
        
        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingAllCardsAndAllowPlayersToChangeVoteAfterScoresShown()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            createNewRoomPage.MarkAllowPlayersToChangeVoteAfterScoresShown();
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential);
            var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
            gameTablePage.TakeAShortNavigationThroughTheMainOptions();
            gameTablePage.InitalizeGameTableWithoutSendingInvitationsAndChooseMultipleCards("2", "3", "5");

            Assert.Equal("5", gameTablePage.CheckWhetherTheCardIsSelectedCorrectly());
            Driver.Dispose();
        }

        [Trait("Category", "Start a quickplay - Sequential Mode")]
        [Fact]
        public void StartAquickPlayInSequentialModeUsingAllCardsAndUploadCSVfile()
        {
            var homePage = new HomePage(Driver);
            var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
            var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
            var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Sequential);
            var gameTablePage = createNewStoryPage.InitCreateNewStory(@"C:\Junior Resources\elements to upload\CSV File.csv");

            Assert.True(gameTablePage.ToastStoryWhenFileWasImportedSuccessfully());
            Driver.Dispose();
        }

        [Trait("Category", "Recent Rooms")]
        [Fact]
        public void ResetAroomByName()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");
            yourPokerRoomPage.ResetACertainRoomByName("qwe");

            Assert.True(yourPokerRoomPage.ToastResetRoomSuccessfully());
            Driver.Dispose();
        }
        
        [Trait("Category", "Recent Rooms")]
        [Fact]
        public void EditAroomByName()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");
            yourPokerRoomPage.EditACertainRoomByName("qwe");

            Assert.True(yourPokerRoomPage.ToastEditRoomSuccessfully());
            Driver.Dispose();
        }
        
        [Trait("Category", "Recent Rooms")]
        [Fact]
        public void DeleteAroomByName()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");
            var numberOfAllRoomsBefore = yourPokerRoomPage.GetTheNumberOfAllRooms();
            yourPokerRoomPage.DeleteACertainRoomByName("asd");

            Assert.NotEqual(yourPokerRoomPage.CheckTheNumberOfRoomsAfterDeleting(), numberOfAllRoomsBefore);
            Driver.Dispose();
        }
        
        [Trait("Category", "My Profile")]
        [Fact]
        public void ChangeTheProfileImage()
        {
            var homePage = new HomePage(Driver);
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");
            var myProfilePage = yourPokerRoomPage.EnterInMyProfile();
            myProfilePage.UploadAndSetTheProfileImage(@"C:\Junior Resources\elements to upload\lynx");

            Assert.True(myProfilePage.CheckWhetherProfileImageWasSuccessfullyUpdated());
            Driver.Dispose();
        }

        //[Trait("Category", "Start a quickplay - Two Players - Scrum")]
        //[Fact]
        //public void StartAquickPlayWithTwoPlayersUsingAllCards()    // Unfinished
        //{
        //    var homePage = new HomePage(Driver);
        //    var startAquickPlayPage = homePage.ClickOnTheStartAquickPlayButton();
        //    var createNewRoomPage = startAquickPlayPage.InitStartAquickPlay("test1");
        //    var createNewStoryPage = createNewRoomPage.InitalizeCreateNewRoom("First Room", Pages.YourPokerRoomsPage.CardType.Scrum);
        //    var gameTablePage = createNewStoryPage.InitCreateNewStory("1. First Story", "2. Second Story");
        //    gameTablePage.TakeAShortNavigationThroughTheMainOptions();
        //    var guestLoginPage = gameTablePage.SendTheLinkToAGuest();
        //    guestLoginPage.FillOutTheNameOfTheSecondPlayerAndSubmit("Guest");
        //    gameTablePage.TakeAShortNavigationThroughTheMainOptions();


        //}

        [Trait("Category", "Random")]
        [Fact]
        public void KeepMeLogedAfterANewChromeTabIsClosed()
        {
            var homePage = new HomePage(Driver);
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
            var loginPage = homePage.ClickOnTheLoginButton();
            var yourPokerRoomPage = loginPage.FillOutTheFormAndSubmit("aaa098873@gmail.com", "ababab.123");
            Thread.Sleep(3000);
            Driver.Close();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Driver.Navigate().GoToUrl(url);
            Assert.Equal("aaa098873", yourPokerRoomPage.GetUserName);
            Driver.Dispose();
        }
    }
}
