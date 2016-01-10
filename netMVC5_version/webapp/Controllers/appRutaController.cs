using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class appRutaController : AuthorizedBaseController
    {
        private static object MapToGridModel(appRutas o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    o.descrip,
                    o.dias,
                    o.activa,
                    o.currLat,
                    o.currLon,
                    currBus = o.appBuses != null ? o.appBuses.nombre : "",
                    currChofer = o.appChoferes != null ? o.appChoferes.nombre : "",
                    o.lastBoard,
                };

        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppRutaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appRutas>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppRutaRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var entity = new appRutas { };
            UnitOfWork.AppRutaRepository.Insert(entity);
            UnitOfWork.Save();

            return PartialView(new appRutaInput { Id = entity.Id, activa = entity.activa, lastBoard=DateTime.Today });
        }

        [HttpPost]
        public ActionResult Create(appRutaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppRutaRepository.GetById(input.Id);
            
            entity.nombre = input.nombre;
            entity.descrip = input.descrip;
            entity.dias = string.Join("-", input.dias);
            entity.activa = input.activa;
            entity.currLat = input.currLat;
            entity.currLon = input.currLon;
            entity.appBuses = UnitOfWork.AppBusRepository.GetById( input.currBus_Id);
            entity.appChoferes = UnitOfWork.AppChofereRepository.GetById(input.currChofer_Id);
            entity.lastBoard = input.lastBoard;

            UnitOfWork.AppRutaRepository.Update(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppRutaRepository.GetById(id);

            var input = new appRutaInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                descrip = entity.descrip,
                dias = string.IsNullOrWhiteSpace(entity.dias) ? null : entity.dias.Split('-').Select(o => int.Parse(o)),
                activa = entity.activa,
                currLat = entity.currLat,
                currLon = entity.currLon,
                currBus_Id = entity.currBus_Id,
                currChofer_Id = entity.currChofer_Id,
                lastBoard = entity.lastBoard,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appRutaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppRutaRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.descrip = input.descrip;
            entity.dias = string.Join("-", input.dias);
            entity.activa = input.activa;
            entity.currLat = input.currLat;
            entity.currLon = input.currLon;
            entity.currBus_Id = input.currBus_Id;
            entity.currChofer_Id = input.currChofer_Id;
            entity.lastBoard = input.lastBoard;

            UnitOfWork.AppRutaRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppRutaRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete excepcione <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppRutaRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }

        public ActionResult GetItemsDayName()
        {
            var dayName = new List<KeyContent>();

            dayName.Add(new KeyContent(1, "Monday"));
            dayName.Add(new KeyContent(2, "Tuesday"));
            dayName.Add(new KeyContent(3, "Wednesday"));
            dayName.Add(new KeyContent(4, "Thursday"));
            dayName.Add(new KeyContent(5, "Friday"));
            dayName.Add(new KeyContent(6, "Saturday"));
            dayName.Add(new KeyContent(0, "Sunday"));

            return Json(dayName);
        }
    }
    /*end*/
}