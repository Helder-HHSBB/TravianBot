using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using SmollRat.driver;
using SmollRat.models.travian;

namespace SmollRat
{
    public class Run
    {
        static void Main(string[] args)
        {
            //we should pass these as arguments from the command line though
        

           
            Console.WriteLine("Hello tell me Acc Name: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            bool flagTrain = false;
            var account = new Account(username, password);
            
            var driver = new TravianIWebDriver();
            
            var loggedIn = account.login(driver);
            if (loggedIn)
            {
                Console.WriteLine("We managed to LogIn ;D");
            }

          




            while (loggedIn)
            {

                do
                {
                     //account.village.getResources(driver);
                     //Console.WriteLine("");
                     //Console.WriteLine ($"Wood: {account.village.resources[Resource.WOOD]}");
                     //Console.WriteLine($"Clay: {account.village.resources[Resource.CLAY]}");
                     //Console.WriteLine($"Iron: {account.village.resources[Resource.IRON]}");
                     //Console.WriteLine($"Grain: {account.village.resources[Resource.WHEAT]}");
                     //Console.WriteLine($"WareHouse: {account.village.resourceCapacity[Resource.WOOD]}");
                     //Console.WriteLine($"Granary: {account.village.resourceCapacity[Resource.WHEAT]}");
                     //Console.WriteLine("");


                    
                    driver.SmallWait();

                    driver.Url = "https://group.europe.travian.com/build.php?tt=99&id=39#";
                    
                    driver.SmallWait();

                    driver.SmallWait();
                    ReadOnlyCollection<IWebElement> farmLists = driver.FindElements(By.XPath(("//*[contains(@class, 'listContent')]")));

                    for (int i = 0; i < farmLists.Count; i++)
                    {
                        driver.Url = "https://group.europe.travian.com/build.php?tt=99&id=39#";
                        driver.SmallWait();
                        driver.SmallWait();
                        farmLists = driver.FindElements(By.XPath(".//*[contains(@class, 'listContent')]"));
                        ReadOnlyCollection<IWebElement> farmRows = farmLists[i].FindElements(By.XPath(".//*[contains(@class, 'slotRow')]"));

                        foreach(var farmRow in farmRows)
                        {
                            if (farmRow.FindElements(By.XPath(".//*[contains(@alt, 'Won as attacker without losses.')]")).Count != 0)
                            {
                                farmRow.FindElement(By.XPath(".//*[contains(@class, 'checkbox')]/input")).Click();
                            }
                        }

                        driver.SmallWait();
                        driver.FindElements(By.XPath("//*[contains(@value, 'Start raid')]"))[i].Click();
                        driver.SmallWait();
                        driver.SmallWait();
                    }





                    DateTime now = DateTime.Now;
                    Console.WriteLine("Time Now: " + now);
                    driver.wait(15,2);
                    
                    //account.village.SendTroopsLegs(driver);
                    var tries = 0;
                    loggedIn = account.isLoggedIn(driver); 
                    if (loggedIn==false) 
                    {
                        tries = 0;
                        do
                        {
                            Console.WriteLine("We were kicked from the server.");
                            Console.WriteLine("Trying to LogIn again...");
                            System.Threading.Thread.Sleep(5000);
                            loggedIn = account.login(driver);
                            tries++;                       
                        } while (loggedIn==false || tries <3);
                        
                        if (tries >= 3) 
                        {
                            Console.WriteLine("Something went very Wrong!!");
                            Console.WriteLine("Aborting...");
                            System.Threading.Thread.Sleep(43200000); //12 Hours Sleeping

                        }
                    } 
                    
                } while (loggedIn);
                
            }

            //example of further code
            /*while (true)
            {
                account.doCriticalActions(driver);
                account.sleep();
            }*/
        }
        
    }
}