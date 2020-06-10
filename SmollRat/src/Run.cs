using System;
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
            Console.WriteLine("Acc Name: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            
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
                     account.village.getResources(driver);
                     Console.WriteLine("");
                     Console.WriteLine ($"Wood: {account.village.resources[Resource.WOOD]}");
                     Console.WriteLine($"Clay: {account.village.resources[Resource.CLAY]}");
                     Console.WriteLine($"Iron: {account.village.resources[Resource.IRON]}");
                     Console.WriteLine($"Grain: {account.village.resources[Resource.WHEAT]}");
                     Console.WriteLine($"WareHouse: {account.village.resourceCapacity[Resource.WOOD]}");
                     Console.WriteLine($"Granary: {account.village.resourceCapacity[Resource.WHEAT]}");
                     Console.WriteLine("");


                    
                    driver.SmallWait();
                    
                  
                    var waitbuild1 = account.village.LvlUpResourcesByOrder(driver, 1);
                    Console.WriteLine(waitbuild1);
                   
                    var waitbuild2 = account.village.LvlUpResourcesByOrder(driver, 2);
                    Console.WriteLine(waitbuild2);
                    
                    driver.SmallWait();
                    
                    account.village.SendFarmList(driver);
                    driver.SmallWait();
                    
                    //Make Imperatoris on First Villa if resources are ok
                    driver.FindElement(By.XPath("//*[@id='sidebarBoxVillagelist']/div[2]/ul/li[1]/a/span[1]/span")).Click();
                    driver.SmallWait();
                    account.village.getResources(driver);
                    if (
                        account.village.resources[Resource.WOOD] > 2500
                        && account.village.resources[Resource.CLAY] > 2500
                        && account.village.resources[Resource.IRON] > 2500
                        && account.village.resources[Resource.WHEAT] > 500

                        )
                    {
                        account.village.TrainImperatoris(driver);
                    }
                    else Console.WriteLine("Not Enough Resources to queue more troops");



                    //*[contains(@class, 'good level colorLayer')]

                    // account.village.TrainBarracks(driver);


                    DateTime now = DateTime.Now;
                    Console.WriteLine("Time Now: " + now);
                    driver.BigWait();
                    
                    //account.village.SendTroopsLegs(driver);
                    
                    loggedIn = account.isLoggedIn(driver); 
                    if (loggedIn==false) 
                    {
                        int tries = 0;
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