using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace PlanITpokerPOM
{
    public class StoriesListPage
    {
        private readonly IWebDriver driver;
        private string storyToChange;
        private string storyChanged;

        public StoriesListPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void SwapTheStoriesOrder(string storyToChange, string storyChanged)
        {
            this.storyToChange = storyToChange;
            this.storyChanged = storyChanged;
            Actions actions = new Actions(driver);
            actions.ClickAndHold(StoriesList[0]);
            actions.Release(StoryNameFromListChanged);
            actions.Build().Perform();
        }

        public void DragAndDrop(string storyToChange, string storyChanged)
        {
            Actions actions = new Actions(driver);
            IWebElement firstOne = driver.FindElement(By.XPath($"//table[@class='table table-striped table-hover']//following::tbody//following::td//following::span[.='{storyToChange}']"));
            IWebElement indexOne = driver.FindElement(By.XPath($"(//span[.='{storyToChange}']//preceding-sibling::img)[1]"));

            IWebElement secondOne = driver.FindElement(By.XPath("//table[@class='table table-striped table-hover']//following::tbody//following::td//following::span[.='1. first']"));
            actions.DragAndDrop(indexOne, StoryNameFromListToChange).Build().Perform();

        }

        private IWebElement ActiveStoriesField => driver.FindElement(By.XPath("//*[contains(text(),'Active Stories ')]"));
        private IWebElement CompletedStoriesField => driver.FindElement(By.XPath("(//*[contains(text(),'Completed Stories ')])[1]"));
        private IWebElement AllStoriesFiled => driver.FindElement(By.XPath("//*[contains(text(),'All Stories ')]"));
        private IWebElement NewButtonToCreateNewStory => driver.FindElement(By.XPath("(//*[@ng-click='createStory()'])[1]"));
        private IWebElement DropdownEditButton => driver.FindElement(By.XPath("(//*[.='Edit'])[1]"));
        private IWebElement ExportStoriesButton => driver.FindElement(By.XPath("(//*[.='Export Stories'])[1]"));
        private IWebElement CustomizeStoriesButton => driver.FindElement(By.XPath("(//*[.='Customize'])[1]"));
        private IWebElement StoryNameFromListToChange => driver.FindElement(By.XPath($"//span[@ng-bind='s.shownTitle' and .='{storyToChange}']"));
        private IWebElement StoryNameFromListChanged => driver.FindElement(By.XPath($"//span[@ng-bind='s.shownTitle' and .='{storyChanged}']"));
        private IWebElement DraggableStoryToChange => driver.FindElement(By.XPath($"(//img[@class='drag-icon normal drag-handle'])[{storyToChange}]"));
        private ReadOnlyCollection<IWebElement> StoriesList => driver.FindElements(By.XPath("//tr[@ng-repeat='s in activeStories']"));
    }
}
