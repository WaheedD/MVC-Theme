using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Helpers;
using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    public class autUsuarioController : AuthorizedBaseController
    {
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.AutUsuarioRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.nroSocio)));
        }
    }
}