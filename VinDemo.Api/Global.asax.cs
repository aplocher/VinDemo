using System.Web;
using System.Web.Http;

namespace VinDemo.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}