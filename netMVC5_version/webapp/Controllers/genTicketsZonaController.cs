using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Helpers;
using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    public class genTicketsZonaController : AuthorizedBaseController
    {   
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.GenTicketsZonaRepository.Get().ToList().Select(o => new KeyContent(o.id, o.desc)));
        }
    }
}