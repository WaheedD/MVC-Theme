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
        public ActionResult GetItem(int? v)
        {
            var o = UnitOfWork.GenTicketsZonaRepository.Get(f => f.id==v).FirstOrDefault();
            return Json(new KeyContent(o.id, o.desc));
        }

        public ActionResult Search(string search, int page)
        {
            const int PageSize = 10;
            search = (search ?? "").ToLower();
            var items = UnitOfWork.GenTicketsZonaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.desc.ToLower().Contains(search));
            }

            items = items.OrderByDescending(x => x.id);

            return Json(new AjaxListResult
            {
                Items = items.Skip((page - 1) * PageSize).Take(PageSize).ToList().Select(o => new KeyContent(o.id, o.desc)),
                More = items.Count() > page * PageSize
            });
        }
    }
}