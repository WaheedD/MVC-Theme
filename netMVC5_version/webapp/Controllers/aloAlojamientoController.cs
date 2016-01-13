using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Helpers;
using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloAlojamientoController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloAlojamientos o)
        {
            return
                new
                {
                    o.Id,
                    o.nroAlojamiento,
                    o.personas,
                    o.inhabilitado,
                    o.precioTempAlta,
                    o.precioTempBaja,
                    o.lat,
                    o.lon,
                    tipo = o.aloTipos != null ? o.aloTipos.nombre : "",
                    o.foto,
                    o.agrup1,
                    o.agrup2,
                    zona = o.zona_Id,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AlojamientoRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.caracteristicas.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloAlojamientos>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AlojamientoRepository.GetById(g.Key), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView(new aloAlojamientoInput { });
        }

        [HttpPost]
        public ActionResult Create(aloAlojamientoInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new aloAlojamientos
            {
                Id = input.Id,
                nroAlojamiento=input.nroAlojamiento,
                personas = input.personas,
                inhabilitado = input.inhabilitado,
                caracteristicas = input.caracteristicas,
                precioTempAlta = input.precioTempAlta,
                precioTempBaja = input.precioTempBaja,
                lat = input.lat,
                lon = input.lon,
                aloTipos =UnitOfWork.AloTipoRepository.GetById( input.tipo),
                foto = input.foto,
                agrup1 = input.agrup1,
                agrup2 = input.agrup2,
                zona_Id = input.zona
            };

            UnitOfWork.AlojamientoRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AlojamientoRepository.GetById(id);

            var input = new aloAlojamientoInput
            {
                Id = entity.Id,
                nroAlojamiento=entity.nroAlojamiento,
                personas = entity.personas,
                inhabilitado = entity.inhabilitado,
                caracteristicas = entity.caracteristicas,
                precioTempAlta = entity.precioTempAlta,
                precioTempBaja = entity.precioTempBaja,
                lat = entity.lat,
                lon = entity.lon,
                tipo = entity.aloTipos.Id,
                foto = entity.foto,
                agrup1 = entity.agrup1,
                agrup2 = entity.agrup2,
                zona = entity.zona_Id
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloAlojamientoInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AlojamientoRepository.GetById(input.Id);

            entity.nroAlojamiento = input.nroAlojamiento;
            entity.personas = input.personas;
            entity.inhabilitado = input.inhabilitado;
            entity.caracteristicas = input.caracteristicas;
            entity.precioTempAlta = input.precioTempAlta;
            entity.precioTempBaja = input.precioTempBaja;
            entity.lat = input.lat;
            entity.lon = input.lon;
            entity.aloTipos =UnitOfWork.AloTipoRepository.GetById( input.tipo);
            entity.foto = input.foto;
            entity.agrup1 = input.agrup1;
            entity.agrup2 = input.agrup2;
            entity.zona_Id = input.zona;

            UnitOfWork.AlojamientoRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(string id, string gridId)
        {
            var entity = UnitOfWork.AlojamientoRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete alojamiento <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AlojamientoRepository.Delete(input.Id);
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}