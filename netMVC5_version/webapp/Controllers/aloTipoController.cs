using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloTipoController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloTipos o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    o.inicioTempAlta,
                    o.finTempAlta,
                    o.clase,
                    o.croquis,
                    o.precioTempAlta,
                    o.precioTempBaja,
                    o.idSede,
                    o.restricFDStempAlta,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloTipoRepository.Get(); 

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloTipos>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AloTipoRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var entity = new aloTipos { inicioTempAlta = DateTime.Today, finTempAlta = DateTime.Today };
            UnitOfWork.AloTipoRepository.Insert(entity);
            UnitOfWork.Save();

            return PartialView(new aloTipoInput { Id = entity.Id });
        }

        [HttpPost]
        public ActionResult Create(aloTipoInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloTipoRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.inicioTempAlta = input.inicioTempAlta;
            entity.finTempAlta = input.finTempAlta;
            entity.clase = input.clase;
            entity.croquis = input.croquis;
            entity.precioTempAlta = input.precioTempAlta;
            entity.precioTempBaja = input.precioTempBaja;
            entity.idSede = input.idSede;
            entity.restricFDStempAlta = input.restricFDStempAlta;

            UnitOfWork.AloTipoRepository.Update(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AloTipoRepository.GetById(id);

            var input = new aloTipoInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                inicioTempAlta = entity.inicioTempAlta,
                finTempAlta = entity.finTempAlta,
                clase = entity.clase,
                croquis = entity.croquis,
                precioTempAlta = entity.precioTempAlta,
                precioTempBaja = entity.precioTempBaja,
                idSede = entity.idSede,
                restricFDStempAlta = entity.restricFDStempAlta,

            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloTipoInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloTipoRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.inicioTempAlta = input.inicioTempAlta;
            entity.finTempAlta = input.finTempAlta;
            entity.clase = input.clase;
            entity.croquis = input.croquis;
            entity.precioTempAlta = input.precioTempAlta;
            entity.precioTempBaja = input.precioTempBaja;
            entity.idSede = input.idSede;
            entity.restricFDStempAlta = input.restricFDStempAlta;

            UnitOfWork.AloTipoRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { entity.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AloTipoRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete tipo <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AloTipoRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}