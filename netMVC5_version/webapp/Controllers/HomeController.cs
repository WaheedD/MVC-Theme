#region Using

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SmartAdminMvc.Models;

#endregion

namespace SmartAdminMvc.Controllers
{
    [SmartAdminMvcAuthorize]
    public class HomeController : Controller
    {
        // GET: home/index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadFile(bool pdf = false)
        {
            if (Request.Files.Count == 0)
            {
                return null;
            }

            var hpf = Request.Files[0] as HttpPostedFileBase;
            if (hpf.ContentLength == 0)
            {
                return null;
            }

            string savedFileName = Path.Combine(Server.MapPath("~/uploads/" + (pdf ? "docs" : "images") + "/"), Path.GetFileName(hpf.FileName));

            // save on filesystem if not already exists
            if (!System.IO.File.Exists(savedFileName))
            {
                hpf.SaveAs(savedFileName);
            }

            return Json(new { fileName = hpf.FileName }, JsonRequestBehavior.AllowGet);
        }
    }
}