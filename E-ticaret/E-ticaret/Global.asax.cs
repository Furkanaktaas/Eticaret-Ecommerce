using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EticaretSitesi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new OtomatikYonlendirme());
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 30;
            //TimeOut özelliğine aktarılacak değer dakika olarak aktarılmaktadır.
        }
    }
}
