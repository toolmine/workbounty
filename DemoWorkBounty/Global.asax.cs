using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DemoWorkBounty
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    //// Get the error details
        //    Exception CurrentException = Server.GetLastError();
        //    //HttpException lastErrorWrapper = Server.GetLastError() as HttpException;
        //    string error = CurrentException.ToString();
        //    string innerException = null;
        //    try
        //    {
        //        innerException = CurrentException.InnerException.InnerException.Message;
        //    }
        //    catch
        //    {
        //    }
        //    string clientID = Session["UserID"].ToString();
        //    ExceptionLog obj = new ExceptionLog();
        //    obj.ClientID = Convert.ToInt32(clientID);
        //    obj.ErrorDetails = error;
        //    obj.InnerException = innerException;
        //    obj.EventDateTime = DateTime.Now.Date;
        //    WorkbountyDBEntities entity = new WorkbountyDBEntities();
        //    entity.ExceptionLogs.Add(obj);
        //    entity.SaveChanges();
        //    Server.ClearError();
        //    Response.Redirect("/Home/Error");
        //}

    }
}