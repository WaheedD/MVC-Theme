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
    public class appParadaController : AuthorizedBaseController
    {
        private static object MapToGridModel(appParada o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    hora = o.hora == 999 ? "Destino final" : o.hora.ToString(),
                    min = o.hora == 999 ? 0 : o.min,
                    estacion = o.appEstacione != null ? o.appEstacione.nombre : "",
                    duracion = o.hora == 999 ? 0 : o.duracion,
                    o.esRetorno,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppParadaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appParada>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppParadaRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }
        public ActionResult GridGetItemsForRuta(GridParams g, int ruta_Id)
        {
            var items = UnitOfWork.AppParadaRepository.Get();

            if (ruta_Id > 0)
            {
                items = items.Where(o => o.ruta_Id == ruta_Id);
            }

            return Json(new GridModelBuilder<appParada>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => UnitOfWork.AppParadaRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int ruta_Id = 0)
        {
            if (ruta_Id > 0)
            {
                return PartialView(new appParadaInput { ruta_Id = ruta_Id, esRetorno = false });
            }
            return PartialView(new appParadaInput { });
        }

        [HttpPost]
        public ActionResult Create(appParadaInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new appParada
            {
                nombre = input.nombre,
                hora = input.hora,
                min = input.hora == 999 ? 0 : input.min,
                CreatedAt = DateTimeOffset.Now,
                Deleted = false,
                estacion_Id = input.estacion_Id,
                ruta_Id = input.ruta_Id,
                duracion = input.hora == 999 ? 0 : input.duracion,
                esRetorno = input.esRetorno,
                currHora = input.hora,
                currMin = input.hora == 999 ? 0 : input.min,
                estDuracion = input.hora == 999 ? 0 : input.duracion,
            };

            UnitOfWork.AppParadaRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppParadaRepository.GetById(id);

            var input = new appParadaInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                hora = entity.hora,
                min = entity.min,
                estacion_Id = entity.estacion_Id,
                duracion = entity.duracion,
                esRetorno = entity.esRetorno,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appParadaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppParadaRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.hora = input.hora;
            entity.min = input.hora == 999 ? 0 : input.min;
            entity.UpdatedAt = DateTimeOffset.Now;
            entity.estacion_Id = input.estacion_Id;
            entity.ruta_Id = input.ruta_Id;
            entity.duracion = input.hora == 999 ? 0 : input.duracion;
            entity.esRetorno = input.esRetorno;
            entity.currHora = input.hora;
            entity.currMin = input.hora == 999 ? 0 : input.min;
            entity.estDuracion = input.hora == 999 ? 0 : input.duracion;

            UnitOfWork.AppParadaRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppParadaRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete parada <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppParadaRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }

        public ActionResult GetItemsMin()
        {
            var min = new List<KeyContent>();

            for (var i = 1; i <= 59; i++)
            {
                min.Add(new KeyContent(i,i.ToString()));
            }

            return Json(min);
        }

        public ActionResult GetItemsHour()
        {
            var hour = new List<KeyContent>();

            for (var i = 1; i <= 24; i++)
            {
                hour.Add(new KeyContent(i, i.ToString()));
            }

            hour.Add(new KeyContent(999, "Destino final"));

            return Json(hour);
        }
    }
    /*end*/
}