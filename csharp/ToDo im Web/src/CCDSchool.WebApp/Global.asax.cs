using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Globalization;
using System.Threading;
namespace CCDSchool.WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {


            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            newCulture.DateTimeFormat.ShortTimePattern = "hh:mm:ss tt";
            newCulture.DateTimeFormat.LongDatePattern = "dd.MM.yyyy";
            newCulture.DateTimeFormat.LongTimePattern = "hh:mm:ss tt";
            Thread.CurrentThread.CurrentCulture = newCulture;

            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}