using System;
using SmollRat.driver;
using SmollRat.models.travian;

namespace SmollRat
{
    public class Run
    {
        static void Main(string[] args)
        {
            //we should pass these as arguments from the command line though
            var username = "OMalhaVacas";
            var password = "amigo12_007581.";
            
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

                    if (account.village.resources[Resource.WOOD] > 1000 
                        && account.village.resources[Resource.CLAY] > 1000 
                        && (account.village.resources[Resource.IRON] > 1000) 
                        && (account.village.resources[Resource.WHEAT] > 500))
                    {
                        account.village.LvlUpResourcesByOrder(driver);
                        account.village.TrainBarracks(driver);
                                                       
                    }
                    else Console.WriteLine("Not enough resources to build");
                    
                    DateTime now = DateTime.Now;
                    Console.WriteLine("Time Now: " + now);
                    driver.SmallWait();
                    account.village.SendTroopsLegs(driver);
                    driver.BigWait();
                    
                    loggedIn = account.isLoggedIn(driver); 
                    if (!loggedIn) 
                    {
                        int tries = 0;
                        do
                        {
                            Console.WriteLine("We were kicked from the server.");
                            Console.WriteLine("Trying to LogIn again...");
                            System.Threading.Thread.Sleep(5000);
                            loggedIn = account.login(driver);
                            tries++;                       
                        } while (!loggedIn || tries <3);
                        
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