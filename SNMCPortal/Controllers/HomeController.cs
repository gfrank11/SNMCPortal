using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snfc.ActiveDirectory;
using Snfc.Impersonation;
using SNMCPortal.Models;
using SNMCDataManager;
using SNMCPortal.Caching;
using SNMCPortal.Filters;
namespace SNMCPortal.Controllers
{
    public class HomeController : Controller
    {
        public enum MenuItemEnum
        {
            None = 0,
            Home = -1,
            Commissions = -2,
            Reports = -3,
            Texting = -4,
            MarketingServices = -5,
            RealEstateFlyer = -6
        }
        public const string UPPER_LEFT = "upper-left";
        public const string UPPER_RIGHT = "upper-right";
        public const string LOWER_LEFT = "lower-left";
        public const string LOWER_RIGHT = "lower-right";
        public const string COMMISSION_URL = "/Home/Commissions";
        public const string PIPELINE_URL = "/Home/Pipeline";
        public const string UNREGISTERED_URL = "/Home/Unregistered";
        //[Authorize(Roles="Admin")]  to restrict to roles on action or controller basis.  Global General authorization is handled in global.asax.cs - AuthorizeAttribute
        public ActionResult Index()
        {
            return View(GetQuadrantRoutes());
        }
        [ChildActionOnly]
        public ActionResult UserName()
        {
            var indexOfSlash = User.Identity.Name.LastIndexOf('\\');
            if (indexOfSlash < 0)
                indexOfSlash = 0;
            else
                indexOfSlash++;
            var loginName = User.Identity.Name.Substring(indexOfSlash);
            string[] names = loginName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var userInfoList = Search.ByFirstAndLastName(names[0], names[1]);
            //make sure employee is placed in cache.
           // var employee = InMemoryCache.GetEmployee(names[0], names[1], true);
            if (userInfoList.Count < 1)
                ViewBag.UserName = loginName.Replace('.', ' ');
            else
                ViewBag.UserName = userInfoList[0].Name;
            return PartialView("UserName");
        }
        public ActionResult RegisterQuadrant(string quadrant, string targetUrl)
        {
            using (var uddaContext = new UserDefinedDataAreaContext())
            {
                if (Url.IsLocalUrl(targetUrl))
                {
                    //update database here user, quadrant, targetUrl
                    var newUdda = new UserDefinedDataArea() { activeDirLogin = User.Identity.Name, areaId = quadrant, targetUrl = targetUrl };
                    var currentUdda = uddaContext.UserDefinedDataAreas.Where(udda => udda.activeDirLogin == newUdda.activeDirLogin && udda.areaId == newUdda.areaId).SingleOrDefault();
                    if (currentUdda != null)
                        currentUdda.targetUrl = targetUrl;
                    else
                        uddaContext.UserDefinedDataAreas.Add(newUdda);
                    uddaContext.SaveChanges();
                    if (targetUrl.Contains("Commissions"))
                        return Commissions();
                    else if (targetUrl.Contains("Pipeline"))
                        return Pipeline();
                }
                return Content("Unregistered.");
            }
        }
        /// <summary>
        /// For content of unregistered quadrants
        /// </summary>
        /// <returns></returns>
        public ActionResult Unregister()
        {
            return Content("unregistered");
        }
        public ActionResult Four01K()
        {
            return PartialView("_Pipeline","401 DATA - " + GetPipelineData());
        }
        public ActionResult Commissions()
        {
            return PartialView("_Commissions", GetCommissionData());
            //return View((object)GetCommissionData()); when you return string, you must make it an object because of how View is defined
        }
        public ActionResult EVM()
        {
            return PartialView("_PipelineQuadrant", "EVM DATA - "+GetPipelineData());
        }
        public ActionResult Pipeline()
        {
            return PartialView("_Pipeline", "PAYROLL DATA - " + GetPipelineData());
        }
        public ActionResult Quadrants()
        {
            return PartialView("_Quadrants", GetQuadrantRoutes());
            //return View((object)GetCommissionData()); when you return string, you must make it an object because of how View is defined
        }
        private IEnumerable<QuadrantRoute> GetQuadrantRoutes()
        {
            using (var uddaContext = new UserDefinedDataAreaContext())
            {
                var uddas = uddaContext.UserDefinedDataAreas.Where(udda => udda.activeDirLogin == User.Identity.Name).ToList();
                var routes = new List<QuadrantRoute>();
                QuadrantRoute route;
                bool ul = false, ur = false, ll = false, lr = false;
                foreach (var udda in uddas)
                {
                    switch (udda.areaId)
                    {
                        case UPPER_LEFT:
                            ul = true;
                            route = new QuadrantRoute { QuadrantName = UPPER_LEFT, Route = udda.targetUrl };
                            routes.Add(route);
                            continue;
                        case UPPER_RIGHT:
                            ur = true;
                            route = new QuadrantRoute { QuadrantName = UPPER_RIGHT, Route = udda.targetUrl };
                            routes.Add(route);
                            continue;
                        case LOWER_LEFT:
                            ll = true;
                            route = new QuadrantRoute { QuadrantName = LOWER_LEFT, Route = udda.targetUrl };
                            routes.Add(route);
                            continue;
                        case LOWER_RIGHT:
                            lr = true;
                            route = new QuadrantRoute { QuadrantName = LOWER_RIGHT, Route = udda.targetUrl };
                            routes.Add(route);
                            continue;
                    };
                }
                if (!ul)
                    routes.Add(new QuadrantRoute() { QuadrantName = UPPER_LEFT, Route = UNREGISTERED_URL });
                if (!ur)
                    routes.Add(new QuadrantRoute() { QuadrantName = UPPER_RIGHT, Route = UNREGISTERED_URL });
                if (!ll)
                    routes.Add(new QuadrantRoute() { QuadrantName = LOWER_LEFT, Route = UNREGISTERED_URL });
                if (!lr)
                    routes.Add(new QuadrantRoute() { QuadrantName = LOWER_RIGHT, Route = UNREGISTERED_URL });
                return routes;
            }
        }
        private string GetPipelineData()
        {
            return HttpUtility.HtmlEncode("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sit amet mauris at sapien iaculis pulvinar. Morbi ut mi in felis fringilla semper. Cras sapien lorem, sodales mattis, pellentesque eu, luctus ac, ante. Curabitur egestas aliquet erat. Nunc accumsan augue egestas dolor. Pellentesque vitae mi. Suspendisse tincidunt elit vel tortor. Sed eu ligula sit amet mi rhoncus ultricies. Curabitur ut risus at lectus semper eleifend. Pellentesque lectus eros, dapibus in, accumsan sit amet, imperdiet eget, dolor. Curabitur et purus sit amet felis mattis aliquam. Etiam feugiat elementum urna. Vivamus iaculis commodo turpis. Donec suscipit, quam eget mollis sagittis, eros nunc feugiat felis, ut sagittis mauris elit a libero. Etiam sed erat. Nullam vestibulum, justo a gravida vulputate, orci dui cursus tellus, dapibus vulputate nibh sem consectetur libero. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam placerat, massa eget dignissim tempor, arcu ipsum condimentum elit, nec ullamcorper felis ante ut justo. Maecenas iaculis purus vel risus. Vestibulum convallis interdum elit. Aliquam sed diam. Sed leo. Etiam sed erat. Nullam vestibulum, justo a gravida vulputate, orci dui cursus tellus, dapibus vulputate nibh sem consectetur libero. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.");
        }
        private string GetCommissionData()
        {
            return HttpUtility.HtmlEncode("THIS IS COMMISSION DATA</br> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sit amet mauris at sapien iaculis pulvinar. Morbi ut mi in felis fringilla semper. Cras sapien lorem, sodales mattis, pellentesque eu, luctus ac, ante. Curabitur egestas aliquet erat. Nunc accumsan augue egestas dolor. Pellentesque vitae mi. Suspendisse tincidunt elit vel tortor. Sed eu ligula sit amet mi rhoncus ultricies. Curabitur ut risus at lectus semper eleifend. Pellentesque lectus eros, dapibus in, accumsan sit amet, imperdiet eget, dolor. Curabitur et purus sit amet felis mattis aliquam. Etiam feugiat elementum urna. Vivamus iaculis commodo turpis. Donec suscipit, quam eget mollis sagittis, eros nunc feugiat felis, ut sagittis mauris elit a libero. Etiam sed erat. Nullam vestibulum, justo a gravida vulputate, orci dui cursus tellus, dapibus vulputate nibh sem consectetur libero. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam placerat, massa eget dignissim tempor, arcu ipsum condimentum elit, nec ullamcorper felis ante ut justo. Maecenas iaculis purus vel risus. Vestibulum convallis interdum elit. Aliquam sed diam. Sed leo. Etiam sed erat. Nullam vestibulum, justo a gravida vulputate, orci dui cursus tellus, dapibus vulputate nibh sem consectetur libero. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam placerat, massa eget dignissim tempor, arcu ipsum condimentum elit, nec ullamcorper felis ante ut justo. Maecenas iaculis purus vel risus. Vestibulum convallis interdum elit. Aliquam sed diam. Sed leo.");
        }
        //protected List<ISiteLink> FullMenu(MenuItemEnum exceptId)
        //{
        //    var menuItems = new List<ISiteLink>();
        //    if (exceptId != MenuItemEnum.Home)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.Home, ParentId = 0, SortOrder = 0, Text = "Home", Url = "/Home/Index" });
        //    if (exceptId != MenuItemEnum.Commissions)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.Commissions, ParentId = 0, SortOrder = 0, Text = "Commissions", Url = "/Home/Commissions", IsDraggable = true });
        //    if (exceptId != MenuItemEnum.MarketingServices)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.MarketingServices, ParentId = 0, SortOrder = 0, Text = "Marketing Services", Url = "/Home/MarketingServices", IsDraggable = true });
        //    if (exceptId != MenuItemEnum.Reports)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.Reports, ParentId = 0, SortOrder = 0, Text = "Reports", Url = "/Home/Reports", IsDraggable = true });
        //    if (exceptId != MenuItemEnum.Texting)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.Texting, ParentId = 0, SortOrder = 0, Text = "Texting", IsDraggable = true });
        //    if (exceptId != MenuItemEnum.RealEstateFlyer)
        //        menuItems.Add(new SiteMenuItem { Id = (int)MenuItemEnum.RealEstateFlyer, ParentId = 0, SortOrder = 0, Text = "Real Estate Flyer", IsDraggable = true });
        //    return menuItems;
        //}
    }
}
