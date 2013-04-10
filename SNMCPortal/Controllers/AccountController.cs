using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNMCPortal.Models;
using Microsoft.Web.WebPages.OAuth;

namespace SNMCPortal.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogin(LoginModel model, string returnUrl)
        {
            return Json(new { success = true, redirect = returnUrl });
            //if (ModelState.IsValid)
            //{
            //    if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            //    {
            //        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            //        return Json(new { success = true, redirect = returnUrl });
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //    }
            //}

            //// If we got this far, something failed
            //return Json(new { errors = GetErrorsFromModelState() });
        }
        public ActionResult Login()
        {
            return PartialView("_Authentication");
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            //return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
            return PartialView("_ExternalLoginsListPartial", new List<AuthenticationClientData>());
        }
    }
}
