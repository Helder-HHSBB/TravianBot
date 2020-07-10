using System;
using OpenQA.Selenium.Support.Events;
using SmollRat.driver;

namespace SmollRat.models.travian
{
    
    public abstract class Page
    {
        public string XPathLocation { get; set; }
        public PageType Name { get; }
        
        public DateTime LastLoad { get; }

        public Page(PageType name, string xPathLocation)
        {
            this.Name = name;
            this.XPathLocation = xPathLocation;
        }

        public TravianIWebDriver navigate(TravianIWebDriver driver)
        {
            driver.clickXPath(this.XPathLocation);
            driver.SmallWait();

            this.parsePage(driver);
            return driver;
        }

        protected abstract bool parsePage(TravianIWebDriver driver);

        public bool refresh(TravianIWebDriver driver, int secondsSinceLastLoad = 60)
        {
            //TODO: Make driver reload page
            if (DateTime.Now > this.LastLoad)
            {
                this.parsePage(driver);
            }

            return true;
        }
        
    }

    public enum PageType
    {
        REPORTS, COMMON
    }
 
    
}