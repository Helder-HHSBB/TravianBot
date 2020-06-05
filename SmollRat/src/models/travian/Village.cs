using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SmollRat.driver;

namespace SmollRat.models.travian
{
    public class Village
    {
        public Dictionary<Resource, double> resources { get; }
        public Dictionary<Resource, double> resourceCapacity { get; }

        private const string PageWoodNumber = "#l1";
        private const string PageClayNumber = "#l2";
        private const string PageIronNumber = "#l3";
        private const string PageWheatNumber = "#l4";
        private const string PageWarehouseCapacity = "#stockBar > div.warehouse > div > div";
        private const string PageSiloCapacity = "#stockBar > div.warehouse > div > div";
        private const string PageResourceViewButton = "#navigation > a.village.resourceView";

        public Village()
        {
            this.resources = new Dictionary<Resource, double>();
            this.resourceCapacity = new Dictionary<Resource, double>();
        }
        private double gatherWareHouseCapacity(IWebDriver driver)
        {
            var warehouse = driver.FindElement(By.CssSelector(PageWarehouseCapacity)).Text;
            warehouse = warehouse.Substring(1, warehouse.Length - 2);
            return double.Parse(warehouse) * 1000;
        }

        private double gatherGrainCapacity(IWebDriver driver)
        {

            var granary = driver.FindElement(By.CssSelector(PageSiloCapacity)).Text;
            granary = granary.Substring(1, granary.Length - 2);
            return double.Parse(granary) * 1000;
        }
        private double getIron(TravianIWebDriver driver)
        {
            var iron = driver.FindElement(By.CssSelector(PageIronNumber)).Text;
            iron = Regex.Replace(iron, "[^0-9 ]", "");
            return double.Parse(iron);
        }

        private double getClay(TravianIWebDriver driver)
        {
            var clay = driver.FindElement(By.CssSelector(PageClayNumber)).Text;
            clay = Regex.Replace(clay, "[^0-9 ]", "");
            return double.Parse(clay);
        }

        private double getWheat(TravianIWebDriver driver)
        {
            var wheat = driver.FindElement(By.CssSelector(PageWheatNumber)).Text;
            wheat = Regex.Replace(wheat, "[^0-9 ]", "");
            return double.Parse(wheat);
        }
        
        private double getWood(TravianIWebDriver driver)
        {
            var wood = driver.FindElement(By.CssSelector(PageWoodNumber)).Text;
            wood = Regex.Replace(wood, "[^0-9 ]", "");
            return double.Parse(wood);
        }

        public bool getResources(TravianIWebDriver driver)
        {
            this.resources[Resource.CLAY] = this.getClay(driver);
            this.resources[Resource.WOOD] = this.getWood(driver);
            this.resources[Resource.IRON] = this.getIron(driver);
            this.resources[Resource.WHEAT] = this.getWheat(driver);

            this.resourceCapacity[Resource.CLAY] = this.gatherWareHouseCapacity(driver);
            this.resourceCapacity[Resource.WOOD] = this.gatherWareHouseCapacity(driver);
            this.resourceCapacity[Resource.IRON] = this.gatherWareHouseCapacity(driver);
            this.resourceCapacity[Resource.WHEAT] = this.gatherGrainCapacity(driver);

            return true;
        }
        
        public bool LvlUpResourcesByOrder(TravianIWebDriver driver)
        {
            driver.FindElement(By.CssSelector(PageResourceViewButton)).Click();
            driver.SmallWait();
            
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
            
            driver.SmallWait();
            
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
            driver.SmallWait();
            driver.FindElement(By.XPath("//*[@id='resourceFieldContainer']/div["+myString+"]")).Click();
            driver.SmallWait();
            try
            {
                driver.FindElement(By.XPath("//*[@class='textButtonV1 green build']")).Click();
                driver.SmallWait();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Não foi possivel evoluir");
            }

            return true;
        }
        
        public bool TrainBarracks (TravianIWebDriver driver)
        {
            driver.Url = "https://ts3.travian.com/build.php?id=31";
            driver.SmallWait();
            
            driver.FindElement(By.CssSelector("#nonFavouriteTroops > div.action.troop.troop1 > div > div.details > div.cta > input")).SendKeys("2");
            driver.SmallWait();
            
            driver.FindElement(By.CssSelector("#s1")).Click();
            Console.WriteLine("Troops in Training");
            driver.SmallWait();
            
            driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click();
            driver.SmallWait();

            return true;
        }

        public bool SendTroopsLegs(TravianIWebDriver driver)
        {
            int[] x = new int[10] {117, 114, 117, 115, 112, 112, 118, 110, 117, 117};
            int[] y = new int[10] { 2, 3, -1, -2, -1, 4, -2, 4, -3, -5};
            
            
            driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click(); 
            driver.SmallWait();
            driver.FindElement(By.CssSelector("#village_map > div.buildingSlot.a39.g16.aid39.roman > div")).Click();
            driver.SmallWait();
            
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
                        driver.SmallWait();
                        driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                        driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                        driver.SmallWait();
                        driver.FindElement(By.CssSelector("#btn_ok")).Click();
                        driver.SmallWait();
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
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                            driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            Console.WriteLine("Ataque " + (i + 1) + " Enviado.");
                        }
                        else if (xTropCount2 < 3)
                        {
                            Console.WriteLine("No troops at HOME :( ");
                            return true;
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
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#yCoordInput")).SendKeys(myString1);
                            driver.FindElement(By.CssSelector("#build > div > form > div.option > label:nth-child(3) > input")).Click();
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            driver.SmallWait();
                            driver.FindElement(By.CssSelector("#btn_ok")).Click();
                            Console.WriteLine("Ataque " + (i + 1) + " Enviado.");
                        }
                        else if (xTropCount2<3)
                        {
                            Console.WriteLine("No troops at HOME :( ");
                            return true;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("No troops at HOME :( ");
                        return true;
                    }



                }
                driver.SmallWait();
                driver.FindElement(By.CssSelector("#navigation > a.village.buildingView")).Click();
                driver.SmallWait();
                driver.FindElement(By.CssSelector("#village_map > div.buildingSlot.a39.g16.aid39.roman > div")).Click();
                driver.SmallWait();
                
            }
            return true;
        }
    }
}