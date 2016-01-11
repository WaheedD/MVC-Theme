using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class appBusController : AuthorizedBaseController
    {
        private static object MapToGridModel(appBuses o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    o.descrip,
                    o.placa,
                    o.pasajeros
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppBusRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appBuses>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppBusRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(appBusInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new appBuses
            {
                nombre = input.nombre,
                descrip = input.descrip,
                placa = input.placa,
                pasajeros = input.pasajeros
            };

            UnitOfWork.AppBusRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppBusRepository.GetById(id);

            var input = new appBusInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                descrip = entity.descrip,
                placa = entity.placa,
                pasajeros = entity.pasajeros
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appBusInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppBusRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.descrip = input.descrip;
            entity.placa = input.placa;
            entity.pasajeros = input.pasajeros;

            UnitOfWork.AppBusRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppBusRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete bus <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppBusRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.AppBusRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.nombre)));
        }
    }
    /*end*/
}