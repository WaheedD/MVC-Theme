﻿using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloExcepcioneController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloExcepciones o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    o.descripcion,
                    o.desde,
                    o.hasta,
                    o.porcentaje,
                    tipo = o.aloTipos != null ? o.aloTipos.nombre : "", //.tipo_Id,
                    o.tipoExcep,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloExcepcioneRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloExcepciones>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AloExcepcioneRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }
        public ActionResult GridGetItemsForTipo(GridParams g, int tipo_Id)
        {
            var items = UnitOfWork.AloExcepcioneRepository.Get();

            if (tipo_Id > 0)
            {
                items = items.Where(o => o.aloTipos.Id == tipo_Id);
            }

            return Json(new GridModelBuilder<aloExcepciones>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => UnitOfWork.AloExcepcioneRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
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
                return PartialView(new aloExcepcioneInput { tipo = tipo_Id, hide_tipo = true });
            }
            return PartialView(new aloExcepcioneInput { });
        }

        [HttpPost]
        public ActionResult Create(aloExcepcioneInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new aloExcepciones
            {
                nombre = input.nombre,
                descripcion = input.descripcion,
                desde = input.desde,
                hasta = input.hasta,
                porcentaje = input.porcentaje,
                aloTipos =UnitOfWork.AloTipoRepository.GetById( input.tipo),
                tipoExcep = input.tipoExcep,
            };

            UnitOfWork.AloExcepcioneRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id, bool hide_tipo = false)
        {
            var entity = UnitOfWork.AloExcepcioneRepository.GetById(id);

            var input = new aloExcepcioneInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                descripcion = entity.descripcion,
                desde = entity.desde,
                hasta = entity.hasta,
                porcentaje = entity.porcentaje,
                tipo = entity.aloTipos.Id,
                hide_tipo = hide_tipo,
                tipoExcep = entity.tipoExcep,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloExcepcioneInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloExcepcioneRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.descripcion = input.descripcion;
            entity.desde = input.desde;
            entity.hasta = input.hasta;
            entity.porcentaje = input.porcentaje;
            entity.aloTipos =UnitOfWork.AloTipoRepository.GetById( input.tipo);
            entity.tipoExcep = input.tipoExcep;

            UnitOfWork.AloExcepcioneRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AloExcepcioneRepository.GetById(id);
            
            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete excepciones <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AloExcepcioneRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();
            
            return Json(new { input.Id });
        }
    }
    /*end*/
}