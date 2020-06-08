using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jQueryAjaxInAsp.NETMVC.Sercurity;

namespace jQueryAjaxInAsp.NETMVC.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult AdminOnly()
        {
            return RedirectToAction("Index", "ConsumableMaterial");
        }

        public ActionResult UnAuthorized()
        {
            return RedirectToAction("ViewOnly", "ConsumableMaterial");
        }
    }
}