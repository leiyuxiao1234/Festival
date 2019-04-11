using APIYuxiao.Models;
using System.Data.Entity;
using System.Web.Http;


namespace APIYuxiao
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IDatabaseInitializer<FestivalContext> init = new DropCreateDatabaseIfModelChanges<FestivalContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new FestivalContext());
        }
    }
}
