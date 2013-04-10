using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
namespace SNMCPortal.Filters
{
    public static class Logger
    {
        private static StreamWriter _sw;
        private static string _user = "";
        public static void WriteLine(string value)
        {
            if (_sw != null)
            {
                if (_user == "")
                    _sw.WriteLine(value);
                else if (_user == HttpContext.Current.User.Identity.Name)
                    _sw.WriteLine(value);
            }
        }
        public static void CloseForUser(string user)
        {
            if (_user == user)
                Close();
        }
        public static void FilterByUser()
        {
            if (_sw != null)
            {
                var setting = System.Web.Configuration.WebConfigurationManager.AppSettings["LogUser"];
                if (setting != null)
                    _user = setting;
            }
        }
        public static void Open()
        {
            if (_sw == null)
            {
                var setting = System.Web.Configuration.WebConfigurationManager.AppSettings["Logging"];
                if (setting != null && setting.ToLower() == "true")
                    _sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/logFile.txt"),true);
            }
            FilterByUser();
        }
        public static void Close()
        {
            if (_sw != null)
            {
                _sw.Flush();
                _sw.Close();
                _sw = null;
            }
        }
    }
    public class LogAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //}
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            Log("OnActionExecuted", filterContext);
        }
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //}
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        private void Log(string methodName, ActionExecutedContext aec)
        {
            var controllerName = aec.RouteData.Values["controller"];
            var actionName = aec.RouteData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Logger.WriteLine(message);
        } 

    }
}