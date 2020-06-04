using System;
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
            Console.WriteLine($"In seconds Sleeping for: {waitMinutes}");
            Console.WriteLine("");
                
            System.Threading.Thread.Sleep(res);
            return true;
        }
    }
}