﻿using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers.Awesome.Lookup
{
    public class AlojamientoLookupController : AuthorizedBaseController
    {
        public ActionResult SearchForm()
        {
            return PartialView();
        }
        public ActionResult Search(string search, int page, bool isTheadEmpty, int? pageSize)
        {
            pageSize = pageSize ?? 10;
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AlojamientoRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.caracteristicas.ToLower().Contains(search));
            }
            items = items.OrderByDescending(x => x.Id);

            var result = new AjaxListResult
            {
                Content = this.RenderPartialView("ListItems/Index", items.Skip((page - 1) * pageSize.Value).Take(pageSize.Value)),
                More = items.Count() > page * pageSize
            };

            if (isTheadEmpty) result.Thead = this.RenderPartialView("ListItems/Thead");
            return Json(result);
        }

        public ActionResult GetItem(int v)
        {
            var o =  v == 0 ? new aloAlojamientos() : UnitOfWork.AlojamientoRepository.GetById(v);

            return Json(new KeyContent(o.Id, o.Id.ToString()));
        }
    }
}