using System;
using SmollRat.driver;

namespace SmollRat.models.travian
{
    public class ReportsPage : Page
    {
        private int lastReadReport;
        
        public ReportsPage() : base(PageType.REPORTS, "/html/body/div[2]/div[2]/div/div[1]/a[6]")
        {
        }

        protected override bool parsePage(TravianIWebDriver driver)
        {
            return true;
        }
    }
}