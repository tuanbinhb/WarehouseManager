using jQueryAjaxInAsp.NETMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jQueryAjaxInAsp.NETMVC.Utility;
using jQueryAjaxInAsp.NETMVC.Sercurity;


namespace jQueryAjaxInAsp.NETMVC.Controllers
{
    [Authorize]
    public class ConsumableMaterialController : Controller
    {
        // GET: ConsumableMaterial
        [AuthorizeRoles("Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewOnly()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllOnly()
        {
            return View(GetAllConsumableMaterial());
        }

        [HttpGet]
        public ActionResult ViewAlll()
        {
            return View(GetAllConsumableMaterial());
        }

        public List<ConsumableMaterialModel> GetAllConsumableMaterial()
        {
            JSONReadWrite reader = new JSONReadWrite();
            var data = reader.returnListConsumable();
            return data;
        }

        public ActionResult AddOrEditConsumable(string id)
        {

            ConsumableMaterialModel emp = new ConsumableMaterialModel();
            JSONReadWrite ex = new JSONReadWrite();
            emp = ex.getConsumableById(id);
            return View(emp);

        }

        [HttpPost]
        public ActionResult AddOrEditConsumable(ConsumableMaterialModel model)
        {
            try
            {

                if (Int32.Parse(model.materialNumberRecipient) > Int32.Parse(model.materialQuantity)) {
                    return Json(new { success = false, message = "Value of 領用數量 cannot be greqter than 請購數量" }, JsonRequestBehavior.AllowGet);
                }

                if (Int32.Parse(model.materialGoodNumberReturn) > Int32.Parse(model.materialNumberRecipient))
                {
                    return Json(new { success = false, message = "Value of 退庫良品數量 cannot be greqter than 領用數量" }, JsonRequestBehavior.AllowGet);
                }
                if (Int32.Parse(model.materialBadNumberReturn) > Int32.Parse(model.materialNumberRecipient)) {
                    return Json(new { success = false, message = "Value of 退庫不良數量 cannot be greqter than 領用數量" }, JsonRequestBehavior.AllowGet);
                }

                if (model.ImageUpload != null && model.materialImagePath != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.materialImagePath = "~/AppFiles/Images/" + fileName;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                JSONReadWrite ex = new JSONReadWrite();
                if (!ex.checkCodeExistOrNot(model.materialCode))
                {
                    var result = ex.addNewConsumable(model);
                    if (result)
                    {
                        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAlll", GetAllConsumableMaterial()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Add new Fail" }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    var result = ex.editConsumable(model);
                    if (result)
                    {
                        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAlll", GetAllConsumableMaterial()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { success = false, message = "Edit Fail" }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                JSONReadWrite ex = new JSONReadWrite();
                var result = ex.deteleMaterialById(id);
                if (result)
                {
                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAlll", GetAllConsumableMaterial()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new { success = false, message = "Delete Fail" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}