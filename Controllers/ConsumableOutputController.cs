using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jQueryAjaxInAsp.NETMVC.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using jQueryAjaxInAsp.NETMVC.Models;

namespace jQueryAjaxInAsp.NETMVC.Controllers
{
    public class ConsumableOutputController : Controller
    {
        // GET: ConsumableOutput
        public ActionResult Index()
        {
            return View(GetAllConsumableOutput());
        }

        public List<ConsumableOutModel> GetAllConsumableOutput()
        {
            JSONReadWrite reader = new JSONReadWrite();
            var data = reader.getListComsumableOut();
            return data;
        }
    }
}