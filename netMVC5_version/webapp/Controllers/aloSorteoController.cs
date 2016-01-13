using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloSorteoController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloSorteos o)
        {
            return
                new
                {
                    o.Id,
                    o.detalle,
                    o.fecha,
                    o.fechaPago,
                    o.estadia_desde,
                    o.estadia_hasta,
                    o.inscripcion_desde,
                    o.inscripcion_hasta,
                    tipo = o.aloTipos != null ? o.aloTipos.nombre : "", //.tipo_Id,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloSorteoRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.detalle.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloSorteos>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AloSorteoRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }
        public ActionResult GridGetItemsForTipo(GridParams g, int tipo_Id)
        {
            var items = UnitOfWork.AloSorteoRepository.Get();

            if (tipo_Id > 0)
            {
                items = items.Where(o => o.tipo_Id == tipo_Id);
            }

            return Json(new GridModelBuilder<aloSorteos>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => UnitOfWork.AloSorteoRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int tipo_Id = 0)
        {
            if (tipo_Id > 0)
            {
                return PartialView(new aloSorteoInput { tipo = tipo_Id, hide_tipo = true });
            }
            return PartialView(new aloSorteoInput {  });
        }

        [HttpPost]
        public ActionResult Create(aloSorteoInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new aloSorteos
                {
                    detalle = input.detalle,
                    fecha = input.fecha,
                    fechaPago = input.fechaPago,
                    estadia_desde = input.estadia_desde,
                    estadia_hasta = input.estadia_hasta,
                    inscripcion_desde = input.inscripcion_desde,
                    inscripcion_hasta = input.inscripcion_hasta,
                    tipo_Id = input.tipo,
                };

            UnitOfWork.AloSorteoRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id, bool hide_tipo = false)
        {
            var entity = UnitOfWork.AloSorteoRepository.GetById(id);

            var input = new aloSorteoInput
            {
                Id = entity.Id,
                detalle = entity.detalle,
                fecha = entity.fecha,
                fechaPago = entity.fechaPago,
                estadia_desde = entity.estadia_desde,
                estadia_hasta = entity.estadia_hasta,
                inscripcion_desde = entity.inscripcion_desde,
                inscripcion_hasta = entity.inscripcion_hasta,
                tipo = entity.aloTipos.Id,
                hide_tipo=hide_tipo,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloSorteoInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloSorteoRepository.GetById(input.Id);

            entity.detalle = input.detalle;
            entity.fecha = input.fecha;
            entity.fechaPago = input.fechaPago;
            entity.estadia_desde = input.estadia_desde;
            entity.estadia_hasta = input.estadia_hasta;
            entity.inscripcion_desde = input.inscripcion_desde;
            entity.inscripcion_hasta = input.inscripcion_hasta;
            entity.tipo_Id = input.tipo;

            UnitOfWork.AloSorteoRepository.Update(entity);
            UnitOfWork.Save();
            //t.InjectFrom(entity);

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AloSorteoRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete sorteo <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AloSorteoRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}

