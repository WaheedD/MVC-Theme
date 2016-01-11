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
        public ActionResult GetItem(int? v)
        {
            var o = UnitOfWork.GenTransaccionesVisanetRepository.Get(f => f.Id == v).FirstOrDefault();
            return Json(new KeyContent(o.Id, o.codConcepto));
        }

        public ActionResult Search(string search, int page)
        {
            const int PageSize = 10;
            search = (search ?? "").ToLower();
            var items = UnitOfWork.GenTransaccionesVisanetRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.codConcepto.ToLower().Contains(search));
            }

            items = items.OrderByDescending(x => x.Id);

            return Json(new AjaxListResult
            {
                Items = items.Skip((page - 1) * PageSize).Take(PageSize).ToList().Select(o => new KeyContent(o.Id, o.codConcepto)),
                More = items.Count() > page * PageSize
            });
        }
    }
}