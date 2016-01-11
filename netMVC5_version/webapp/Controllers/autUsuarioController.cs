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
        public ActionResult getPersonal()
        {
            return Json(UnitOfWork.AutUsuarioRepository.Get(o => o.personalCRL).ToList().Select(o => new KeyContent(o.Id, o.nombre + " (" + o.email + ")")));
        }

        public ActionResult GetItem(string v)
        {
            v = (v ?? "").ToLower();
            var o = UnitOfWork.AutUsuarioRepository.Get(f => f.Id.ToLower().Contains(v)).FirstOrDefault();
            return Json(new KeyContent(o.Id, o.nombre));
        }

        public ActionResult Search(string search, int page)
        {
            const int PageSize = 10;
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AutUsuarioRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            items = items.OrderByDescending(x => x.Id);

            return Json(new AjaxListResult
            {
                Items = items.Skip((page - 1) * PageSize).Take(PageSize).ToList().Select(o => new KeyContent(o.Id, o.nombre)),
                More = items.Count() > page * PageSize
            });
        }
    }
}