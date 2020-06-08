using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Security;
using jQueryAjaxInAsp.NETMVC.Models;
using jQueryAjaxInAsp.NETMVC.Utility;
using System.Security.Principal;

namespace jQueryAjaxInAsp.NETMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult SignOut()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserLogin model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                JSONReadWrite UM = new JSONReadWrite();
                string password = UM.GetUserPassword(model.UserName);

                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                else
                {
                    if (model.PassWord.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("AdminOnly", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The password provided is incorrect.");
                    }
                }
            }
            return View(model);
        }
    }
}