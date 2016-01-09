using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Helpers;
using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    public class genTransaccionesVisanetController : AuthorizedBaseController
    {
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.GenTransaccionesVisanetRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.codConcepto)));
        }
    }
}