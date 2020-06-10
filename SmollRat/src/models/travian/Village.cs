using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
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
        private const string PageVillage1 = "#sidebarBoxVillagelist > div.content > ul > li:nth-child(1) > a > span.iconAndNameWrapper > span";
        private const string PageVillage2 = "#sidebarBoxVillagelist > div.content > ul > li:nth-child(2) > a > span.iconAndNameWrapper > span";

        public Village() 
        {
            this.resources = new Dictionary<Resource, double>();
            this.resourceCapacity = new Dictionary<Resource, double>();
        }
        private double gatherWareHouseCapacity(IWebDriver driver)
        {
            
            var warehouse = driver.FindElement(By.CssSelector(PageWarehouseCapacity)).Text;
            warehouse = warehouse.Substring(1, warehouse.Length - 2);
            return double.Parse(warehouse);
        }

        private double gatherGrainCapacity(IWebDriver driver)
        {
            
            var granary = driver.FindElement(By.CssSelector(PageSiloCapacity)).Text;
            granary = granary.Substring(1, granary.Length - 2);
            return double.Parse(granary);
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
        
            
        public int LvlUpResourcesByOrder(TravianIWebDriver driver, int villageid)
        {
            if (villageid == 1)
            {
                driver.FindElement(By.CssSelector(PageVillage1)).Click();
            }
            if (villageid == 2)
            {
                driver.FindElement(By.CssSelector(PageVillage2)).Click();
            }
            
            driver.SmallWait();
            driver.FindElement(By.CssSelector(PageResourceViewButton)).Click();

            try
            {
                int lowestIndex = 0;
                
                

                if (driver.FindElement(By.XPath("//*[contains(@class, 'good level colorLayer')]")) != null)
                {
                    var listGoodFields = driver.FindElements(By.XPath("//*[contains(@class, 'good level colorLayer')]"));
                    var cleanTempGoodFieldText = Regex.Replace(listGoodFields[0].Text, "[^0-9 ]", "");

                    int lowestLvl = int.Parse(cleanTempGoodFieldText);

                    for (int i = 0; i < listGoodFields.Count; i++)
                    {
                        var goodFieldText = listGoodFields[i].Text;
                        var cleanGoodFieldText = Regex.Replace(goodFieldText, "[^0-9 ]", "");
                        var intCleanGoodFieldText = int.Parse(cleanGoodFieldText);
                        if (intCleanGoodFieldText < lowestLvl)
                        {
                            lowestIndex = i;
                        }

                    }
                    Console.WriteLine("Hello im the lowest field with id " + lowestIndex + 1);

                    listGoodFields[lowestIndex].Click();
                    driver.SmallWait();

                    var pageUpgradingTime = driver.FindElement(By.XPath("//*[@id='build']/div[3]/div[3]/div[1]/div/span")).Text;

                    double doubleTimeMs = TimeSpan.Parse(pageUpgradingTime).TotalMilliseconds;
                    Console.WriteLine("Upgrading a Resource Field...");
                    Console.WriteLine(pageUpgradingTime);
                    int cleanTimeMs = (int)doubleTimeMs;

                    driver.FindElement(By.XPath("//*[@class='textButtonV1 green build']")).Click();
                    driver.SmallWait();
                    return (cleanTimeMs);

                }
                else return -5;
            }
            catch (NoSuchElementException)
            {

                Console.WriteLine("Nothing to lvl Up now");
                return -1;
               
            }
          
        }



        public bool TrainImperatoris(TravianIWebDriver driver)
        {
          
               

            driver.FindElement(By.XPath("//*[contains(@class, 'stable')]")).Click();
            driver.SmallWait();

            driver.FindElement(By.XPath("//*[contains(@name, 't5')]")).SendKeys("1");
            driver.SmallWait();
            

            driver.FindElement(By.XPath("//*[@id='s1']")).Click();
            driver.SmallWait();

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


        public void SendFarmList(TravianIWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='sidebarBoxLinklist']/div[2]/ul/li/a")).Click();
            driver.SmallWait();
            driver.FindElement(By.XPath("//*[@id='raidListMarkAll1121']")).Click();
            driver.SmallWait();
            driver.FindElement(By.XPath("//*[contains(@value, 'Começar assalto')]")).Click();

        }

        
    }
}








