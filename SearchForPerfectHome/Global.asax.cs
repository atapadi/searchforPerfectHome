using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SearchForPerfectHome
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            // ***
            // *** Get the exception
            // ***
            Exception exception = Server.GetLastError();
            log.Error(exception);
            // ***
            // *** Clear out any existing response
            // ***
            Response.Clear();
            //CacheHelper.Set(CacheKeys.Exception, exception);
            // ***
            // *** Clear the server error or the view will not display
            // ***
            HttpContext.Current.Server.ClearError();
           // Response.Redirect("~/Error/Index");
        }

    }
}
