using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloZonaController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloZonas o)
        {
            return
                new
                {
                    o.Id,
                    o.desc1,
                    o.desc2,
                    o.personas,
                    o.croquis,
                    o.precioTempAlta,
                    o.precioTempBaja,
                    tipo = o.tipo_Id
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloZonaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.desc1.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloZonas>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AloZonaRepository.GetById(g.Key), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView(new aloZonaInput { });
        }

        [HttpPost]
        public ActionResult Create(aloZonaInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new aloZonas
            {
                Id = input.Id,
                desc1 = input.desc1,
                desc2 = input.desc2,
                personas = input.personas,
                croquis = input.croquis,
                precioTempAlta = input.precioAlta,
                precioTempBaja= input.precioBaja,
                tipo_Id = input.tipo??0,
            };

            UnitOfWork.AloZonaRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(string id)
        {
            var entity = UnitOfWork.AloZonaRepository.GetById(id);

            var input = new aloZonaInput
            {
                Id = entity.Id,
                desc1 = entity.desc1,
                desc2 = entity.desc2,
                personas = entity.personas,
                croquis = entity.croquis,
                precioAlta = entity.precioTempAlta??0,
                precioBaja = entity.precioTempBaja??0,
                tipo = entity.tipo_Id,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloZonaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloZonaRepository.GetById(input.Id);

            entity.desc1 = input.desc1;
            entity.desc2 = input.desc2;
            entity.personas = input.personas;
            entity.croquis = input.croquis;
            entity.precioTempAlta = input.precioAlta;
            entity.precioTempBaja = input.precioBaja;
            entity.tipo_Id = input.tipo??0;

            UnitOfWork.AloZonaRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(string id, string gridId)
        {
            var entity = UnitOfWork.AloZonaRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete zona <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AloZonaRepository.Delete(input.Id);
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}