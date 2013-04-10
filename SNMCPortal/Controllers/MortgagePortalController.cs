using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNMCPortal.Controllers
{
    public class MortgagePortalController : Controller
    {
        //
        // GET: /MortgagePortal/
        HomeController _home;
        public MortgagePortalController()
        {
            _home = new HomeController();
        }
        public ActionResult Index()
        {
            return _home.Index();
        }
        public ActionResult RegisterQuadrant(string quadrant, string targetUrl)
        {
            return _home.RegisterQuadrant(quadrant, targetUrl);
        }
        public ActionResult Commissions()
        {
            return _home.Commissions();
        }
        public ActionResult Pipeline()
        {
            return _home.Pipeline();
        }
        public ActionResult Unregister()
        {
            return _home.Unregister();
        }
    }
}
