using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SmollRat.driver
{
    public class TravianIWebDriver : ChromeDriver
    {
        public bool SmallWait()
        {        
            Random rand = new Random();
                
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = 3000 + 500 * randStdNormal; //(mean,stdDev^2)
                
            int res = Convert.ToInt32(randNormal);
            if (res < 400) { res = res + 400; }
                
            string waitMinutes = TimeSpan.FromMilliseconds(res).TotalSeconds.ToString();
            
                
            System.Threading.Thread.Sleep(res);
            return true;
        }

        public int SmallSleep(int x)
        {
            Random rand = new Random();

            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = x + 2500 * randStdNormal; //(mean,stdDev^2)

            int res = Convert.ToInt32(randNormal);
            if (res < 400) { res = res + 400; }

            string waitMinutes = TimeSpan.FromMilliseconds(res).TotalSeconds.ToString();
            return res;
        }

        public void BigWait()
        {
            Random rand = new Random();
            
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = 1000000 + 200000 * randStdNormal; //(mean,stdDev^2)
            
            int res = Convert.ToInt32(randNormal);
            if (res < 120000) { res = res + 20000; }
            
            double waitMinutes = TimeSpan.FromMilliseconds(res).TotalMinutes;
            Console.WriteLine("In minutes Sleeping for: " + waitMinutes);
            Console.WriteLine("");
            
            System.Threading.Thread.Sleep(res);
        }
        
        public void wait(int meanMinutes, int deviationMinutes)
        {
            Random rand = new Random();
            
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = (meanMinutes * 60 * 1000) + (deviationMinutes * 60 * 1000) * randStdNormal; //(mean,stdDev^2)
            
            int res = Convert.ToInt32(randNormal);
            if (res < 120000) { res = res + 20000; }
            
            double waitMinutes = TimeSpan.FromMilliseconds(res).TotalMinutes;
            Console.WriteLine("In minutes Sleeping for: " + waitMinutes);
            Console.WriteLine("");
            
            System.Threading.Thread.Sleep(res);
        }

        
        public bool clickXPath(string xPath) {
            this.FindElement(By.XPath((xPath))).Click();
            this.SmallWait();
            return true;
        }
    }
}