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
            account.login(driver);
            
            //example of further code
            /*while (true)
            {
                account.doCriticalActions(driver);
                account.sleep();
            }*/
        }
        
    }
}