﻿using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using Omu.AwesomeMvc;
using System;

namespace SmartAdminMvc.Controllers.Awesome.Lookup
{
    /*begin*/
    public class ZonaLookupController : AuthorizedBaseController
    {
        public ActionResult SearchForm()
        {
            return PartialView();
        }
        public ActionResult Search(string search, int page, bool isTheadEmpty, int? pageSize)
        {
            pageSize = pageSize ?? 10;

            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloZonaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.desc1.ToLower().Contains(search));
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

        public ActionResult GetItems(string v,string parent)
        {
            var i = Convert.ToInt32(parent);
            var zonas = UnitOfWork.AloZonaRepository.Get(o => o.tipo_Id == i);

            return Json(zonas.Select(o => new KeyContent(o.Id, o.Id)));
        }

        public ActionResult GetItem(string v)
        {
            
            var o = string.IsNullOrWhiteSpace(v) || v == "0" ? new aloZonas() : UnitOfWork.AloZonaRepository.GetById(v);

            return Json(new KeyContent(o.Id, o.Id));
        }
    }
    /*end*/
}