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
            return Json(UnitOfWork.AutUsuarioRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.nombre)));
        }

        public ActionResult getPersonal()
        {
            return Json(UnitOfWork.AutUsuarioRepository.Get(o => o.personalCRL).ToList().Select(o => new KeyContent(o.Id, o.nombre + " (" + o.email + ")")));
        }
    }
}