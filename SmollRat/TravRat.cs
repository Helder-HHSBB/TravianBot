using System;
using System.Linq.Expressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.Linq;

namespace SmollRat
{
    class TravRat
    {

        static void Main(string[] args)
        {
            double wood, clay, iron, grain, warehouse, granary;
            bool firstlogstatus = false, logstatus = false;
            
            IWebDriver driver = new ChromeDriver();
            
            


            firstlogstatus = TryToLogIn(driver);
            if(firstlogstatus==true) { Console.WriteLine("We managed to LogIn ;D"); }
           
            
            while (firstlogstatus == true)
            {

                do
                {
                    wood = GatherWood(driver);
                    clay = GatherClay(driver);
                    iron = GatherIron(driver);
                    grain = GatherGrain(driver);
                    warehouse = GatherWareHouseCapacity(driver);
                    granary = GatherGrainCapacity(driver);

                    Console.WriteLine("");
                    Console.WriteLine ("Wood: {0}",wood);
                    Console.WriteLine("Clay: {0}", clay);
                    Console.WriteLine("Iron: {0}", iron);
                    Console.WriteLine("Grain: {0}", grain);
                    Console.WriteLine("WareHouse: {0}", warehouse);
                    Console.WriteLine("Granary: {0}", granary);
                    Console.WriteLine("");

                    if ((wood > 1000) && (clay > 1000) && (iron > 1000) && (grain > 500))
                    {
                        LvlUpResourcesByOrder(driver, wood, clay, iron, grain);
                        TrainBarracks(driver);
                                                       
                    }
                    else Console.WriteLine("Not enough resources to build");
                    
                    DateTime now = DateTime.Now;
                    Console.WriteLine("Time Now: " + now);
                    SmallWait();
                    SendTroopsLegs(driver);
                    BigWait();
                    
                    logstatus = CheckLogInStatus(driver); 
                    if (logstatus==false) 
                    {
                        int tries = 0;
                        do
                        {
                            Console.WriteLine("We were kicked from the server.");
                            Console.WriteLine("Trying to LogIn again...");
                            System.Threading.Thread.Sleep(5000);
                            logstatus = TryToLogIn(driver);
                            tries++;                       
                        } while (logstatus == false || tries <3);
                        
                        if (tries >= 3) 
                        {
                            Console.WriteLine("Something went very Wrong!!");
                            Console.WriteLine("Aborting...");
                            System.Threading.Thread.Sleep(43200000); //12 Hours Sleeping

                        }
                    } 
                    
                } while (logstatus == true);
                
            }
            
        }

        private static void SendTroopsLegs(IWebDriver driver)
        {
            int[] x = new int[10] {117, 114, 117, 115, 112, 112, 118, 110, 117, 117};
            int[] y = new int[10] { 2, 3, -1, -2, -1, 4, -2, 4, -3, -5};
            
            
            driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click(); 
            SmallWait();
            driver.FindElement(By.CssSelector("#village_map > div.buildingSlot.a39.g16.aid39.roman > div")).Click();
            SmallWait();
            
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(1) > td.line-first.column-first.large.‭ > a"));
                    var auxTroopCount = driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(1) > td.line-first.column-first.large.‭ > a"));
                    var troopCount = auxTroopCount.Text;
                    var cleanTroop = Regex.Replace(troopCount, "[^0-9 ]", "");
                    var xTropCount = double.Parse(cleanTroop);

                    if (xTropCount >= 3)
                    {
                        driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(1) > td.line-first.column-first.large.‭ > input")).SendKeys("3");
                        string myString = x[i].ToString();
                        string myString1 = y[i].ToString();
                        driver.FindElement(By.CssSelector("#xCoordInput")).SendKeys(myString);
                        SmallWait();
                        driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                        driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                        SmallWait();
                        driver.FindElement(By.CssSelector("#btn_ok")).Click();
                        SmallWait();
                        driver.FindElement(By.CssSelector("#btn_ok")).Click();

                        Console.WriteLine("Ataque " + (i + 1) + " Enviado.");
                    }
                    if (xTropCount < 3)
                    {
                        driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > a"));
                        var auxTroopCount2 = driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > a"));
                        var troopCount2 = auxTroopCount2.Text;
                        var cleanTroop2 = Regex.Replace(troopCount2, "[^0-9 ]", "");
                        var xTropCount2 = double.Parse(cleanTroop2);
                        if (xTropCount2 >= 3)
                        {
                            driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > input")).SendKeys("3");
                            string myString = x[i].ToString();
                            string myString1 = y[i].ToString();
                            driver.FindElement(By.CssSelector("#xCoordInput")).SendKeys(myString);
                            SmallWait();
                            driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                            driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                            SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            Console.WriteLine("Ataque " + (i + 1) + " Enviado.");
                        }
                        else if (xTropCount2 < 3)
                        {
                            Console.WriteLine("No troops at HOME :( ");
                            return;
                        }
                    }

                }
                catch (NoSuchElementException)
                {
                    try
                    {
                        driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > a"));
                        var auxTroopCount2 = driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > a"));
                        var troopCount2 = auxTroopCount2.Text;
                        var cleanTroop2 = Regex.Replace(troopCount2, "[^0-9 ]", "");
                        var xTropCount2 = double.Parse(cleanTroop2);
                        if (xTropCount2 >= 3)
                        {
                            driver.FindElement(By.CssSelector("#troops > tbody > tr:nth-child(3) > td.line-last.column-first.large.‭ > input")).SendKeys("3");
                            string myString = x[i].ToString();
                            string myString1 = y[i].ToString();
                            driver.FindElement(By.CssSelector("#xCoordInput")).SendKeys(myString);
                            SmallWait();
                            driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                            driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                            SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            Console.WriteLine("Ataque " + (i + 1) + " Enviado.");
                        }
                        else if (xTropCount2<3)
                        {
                            Console.WriteLine("No troops at HOME :( ");
                            return;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("No troops at HOME :( ");
                        return;
                    }



                }
                SmallWait();
                driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click();
                SmallWait();
                driver.FindElement(By.CssSelector("#village_map > div.buildingSlot.a39.g16.aid39.roman > div")).Click();
                SmallWait();

            }
        }




        private static bool CheckLogInStatus (IWebDriver driver)
        {
            driver.Url = "https://ts3.travian.com/dorf1.php";
            SmallWait();
            
            try
            {
                driver.FindElement(By.CssSelector("#sidebarBoxActiveVillage > div.content > div.playerName"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static void LvlUpResourcesByOrder(IWebDriver driver, double wood, double clay, double iron, double grain)
        {
            driver.FindElement(By.CssSelector("#navigation > a.village.resourceView")).Click();
            SmallWait();
            
            string[] xPath = new string[18];
            int[] xRes = new int[18];
            xPath[0] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[1]")).Text;
            xPath[1] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[2]")).Text;
            xPath[2] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[3]")).Text;
            xPath[3] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[4]")).Text;
            xPath[4] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[5]")).Text;
            xPath[5] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[6]")).Text;
            xPath[6] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[7]")).Text;
            xPath[7] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[8]")).Text;
            xPath[8] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[9]")).Text;
            xPath[9] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[10]")).Text;
            xPath[10] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[11]")).Text;
            xPath[11] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[12]")).Text;
            xPath[12] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[13]")).Text;
            xPath[13] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[14]")).Text;
            xPath[14] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[15]")).Text;
            xPath[15] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[16]")).Text;
            xPath[16] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[17]")).Text;
            xPath[17] = driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div[18]")).Text;
            

            SmallWait();
            
            int i = 0, menor=20, lesserI = 0;
            
            while (i < 18)
            {
                xRes[i] = int.Parse(xPath[i]);
                Console.WriteLine(xRes[i]);
                if (xRes[i]<=menor)
                {
                    lesserI = i+1;
                    menor = xRes[i];
                }
                i++;            
            }

          

            Console.WriteLine(menor);
            Console.WriteLine(lesserI);
            string myString = lesserI.ToString();
            Console.WriteLine(myString);
            SmallWait();
            driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div["+myString+"]")).Click();
            SmallWait();
            try
            {
                driver.FindElement(By.XPath("//*[@class='textButtonV1 green build']")).Click();
                SmallWait();
                return;


            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Não foi possivel evoluir");
                return;
            }
        }
             
                
                
                
            
        private static double GatherWood(IWebDriver driver)
        {

            var auxWood = driver.FindElement(By.CssSelector("#l1"));
            var wood = auxWood.Text;
            var clean = Regex.Replace(wood, "[^0-9 ]", "");
            var xWood = double.Parse(clean);
            
            return (xWood);
        }
            
       

        private static double GatherClay(IWebDriver driver)
        {

            var auxClay = driver.FindElement(By.CssSelector("#l2"));
            var clay = auxClay.Text;
            var clean = Regex.Replace(clay, "[^0-9 ]", "");
            var xclay = double.Parse(clean);
            
            return (xclay);
        }

        private static double GatherIron (IWebDriver driver)
        {

            var auxIron = driver.FindElement(By.CssSelector("#l3"));
            var iron = auxIron.Text;
            var clean = Regex.Replace(iron, "[^0-9 ]", "");
            var xIron = double.Parse(clean);
          
            return (xIron);
        }

        private static double GatherGrain(IWebDriver driver)
        {
            var auxGrain = driver.FindElement(By.CssSelector("#l4"));
            var grain = auxGrain.Text;
            var clean = Regex.Replace(grain, "[^0-9 ]", "");
            var xGrain = double.Parse(clean);
            
            return (xGrain);
        }

        private static double GatherWareHouseCapacity(IWebDriver driver)
        {

            var auxWareHouse = driver.FindElement(By.CssSelector("#stockBar > div.warehouse > div > div"));
            var wareHouse = auxWareHouse.Text;
            wareHouse = wareHouse.Substring(1, wareHouse.Length - 2);
            var xWareHouse = double.Parse(wareHouse);
            xWareHouse = (xWareHouse * 1000);
           
            return (xWareHouse);
        }

        private static double GatherGrainCapacity(IWebDriver driver)
        {

            var auxGranary = driver.FindElement(By.CssSelector("#stockBar > div.granary > div > div"));
            var granary = auxGranary.Text;
            granary = granary.Substring(1, granary.Length - 2);
            var xGranary = double.Parse(granary);
            xGranary = (xGranary * 1000);
            
            return (xGranary);
        }





        private static void TrainBarracks (IWebDriver driver)
        {
            driver.Url = "https://ts3.travian.com/build.php?id=31";
            SmallWait();
            
            driver.FindElement(By.CssSelector("#nonFavouriteTroops > div.action.troop.troop1 > div > div.details > div.cta > input")).SendKeys("2");
            SmallWait();
            
            driver.FindElement(By.CssSelector("#s1")).Click();
            Console.WriteLine("Troops in Training");
            SmallWait();
            
            driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click();
            SmallWait();
        }

            
           
            
            
        private static void RandomEvents ()
        {
            int i = 0;
            Random rng = new Random();
            double r = Math.Sqrt(-2 * Math.Log(rng.NextDouble()));
            double θ = 2 * Math.PI * rng.NextDouble();
            double x = r * Math.Cos(θ);
            double y = r * Math.Sin(θ);

            do
            {
                Console.WriteLine(x);
                Console.WriteLine(y);
                i++;            
            } while (i <= 20);        
        
        }

        private static void SmallWait()
        {        
                Random rand = new Random();
                
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                double randNormal = 3000 + 500 * randStdNormal; //(mean,stdDev^2)
                
                int res = Convert.ToInt32(randNormal);
                if (res < 400) { res = res + 400; }
                
                double waitMinutes = TimeSpan.FromMilliseconds(res).TotalSeconds;
                Console.WriteLine("In seconds Sleeping for: " + waitMinutes);
                Console.WriteLine("");
                
                System.Threading.Thread.Sleep(res);
        }

        private static void BigWait()
        {
            Random rand = new Random();
            
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = 1500000 + 99999 * randStdNormal; //(mean,stdDev^2)
            
            int res = Convert.ToInt32(randNormal);
            if (res < 720000) { res = res + 20000; }
            
            double waitMinutes = TimeSpan.FromMilliseconds(res).TotalMinutes;
            Console.WriteLine("In minutes Sleeping for: " + waitMinutes);
            Console.WriteLine("");
            
            System.Threading.Thread.Sleep(res);
        }

        private static bool TryToLogIn(IWebDriver driver)
        {
            driver.Url = "https://ts3.travian.com/login.php";
            driver.FindElement(By.CssSelector("#content > div.outerLoginBox > div.innerLoginBox > form > table > tbody > tr.account > td:nth-child(2) > input")).SendKeys("OMalhaVacas");
            driver.FindElement(By.CssSelector("#content > div.outerLoginBox > div.innerLoginBox > form > table > tbody > tr.pass > td:nth-child(2) > input")).SendKeys("amigo12_007581.");
            
            SmallWait();
            
            driver.FindElement(By.CssSelector("#s1")).Click();

            SmallWait();
           

            try
            {
                driver.FindElement(By.CssSelector("#sidebarBoxActiveVillage > div.content > div.playerName"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;        
            }

        }    
    }
}


            
            
            
            
            


