using System.Web.Http;
using System.Web.Routing;

namespace myshopService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}