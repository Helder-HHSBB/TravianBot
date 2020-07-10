using SmollRat.driver;

namespace SmollRat.models.travian
{
    public class CommonPage : Page
    {
        public CommonPage(PageType name, string xPathLocation) : base(name, xPathLocation)
        {
        }

        protected override bool parsePage(TravianIWebDriver driver)
        {
            throw new System.NotImplementedException();
        }
    }
}