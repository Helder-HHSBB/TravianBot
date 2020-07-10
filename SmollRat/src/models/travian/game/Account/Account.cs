using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using SmollRat.driver;

namespace SmollRat.models.travian
{
    public class Account
    {
        private string username;
        private string password;
        
        private const string LoginUrl = "https://group.europe.travian.com/login.php";
        private const string PageLoginBox =
            "#content > div.outerLoginBox > div.innerLoginBox > form > table > tbody > tr.account > td:nth-child(2) > input";
        private const string PagePasswordBox =
            "#content > div.outerLoginBox > div.innerLoginBox > form > table > tbody > tr.pass > td:nth-child(2) > input";
        private const string PageLoginButton = "#s1";
        private const string PagePlayerName = "#sidebarBoxActiveVillage > div.content > div.playerName";

        public Village village { get; set; }

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.village = new Village();
        }

        public bool login(TravianIWebDriver driver)
        {
            driver.Url = LoginUrl;
            driver.SmallWait();
            driver.FindElement(By.CssSelector(PageLoginBox)).SendKeys(this.username);
            driver.FindElement(By.CssSelector(PagePasswordBox)).SendKeys(this.password);
            
            driver.SmallWait();
            
            driver.FindElement(By.CssSelector(PageLoginButton)).Click();

            driver.SmallWait();

            try
            {
                driver.FindElement(By.CssSelector(PagePlayerName));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool isLoggedIn(TravianIWebDriver driver)
        {
            try
            {
                driver.FindElement(By.CssSelector(PagePlayerName));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;        
            }
        }

    }
}