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
    public class appPromocioneController : AuthorizedBaseController
    {
        private static object MapToGridModel(appPromociones o)
        {
            return
                new
                {
                    o.Id,
                    o.desc,
                    o.foto,
                    o.desde,
                    o.hasta,
                    o.sexo,
                    o.edadMax,
                    o.edadMin,
                    o.tipoAsociado,
                    o.nroCupones,
                    o.marca
                };

        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppPromocioneRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.desc.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appPromociones>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppPromocioneRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
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
        public ActionResult Create(appPromocioneInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new appPromociones
            {
                desc = input.desc,
                detalle = input.detalle,
                foto = Helper.movePhoto(input.foto, "promos"),
                desde = input.desde,
                hasta = input.hasta,
                sexo = input.sexo,
                edadMax = input.edadMax,
                edadMin = input.edadMin,
                tipoAsociado = input.tipoAsociado,
                nroCupones = input.nroCupones,
                marca = input.marca,
                email = input.email,
                tel = input.tel,
                direccion = input.direccion,
            };

            UnitOfWork.AppPromocioneRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppPromocioneRepository.GetById(id);

            var input = new appPromocioneInput
            {
                Id = entity.Id,
                desc = entity.desc,
                detalle = entity.detalle,
                foto = entity.foto,
                desde = entity.desde,
                hasta = entity.hasta,
                sexo = entity.sexo,
                edadMax = entity.edadMax,
                edadMin = entity.edadMin,
                tipoAsociado = entity.tipoAsociado,
                nroCupones = entity.nroCupones,
                marca = entity.marca,
                email = entity.email,
                tel = entity.tel,
                direccion = entity.direccion,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appPromocioneInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppPromocioneRepository.GetById(input.Id);

            entity.desc = input.desc;
            entity.detalle = input.detalle;
            entity.desde = input.desde;
            entity.hasta = input.hasta;
            entity.sexo = input.sexo;
            entity.edadMax = input.edadMax;
            entity.edadMin = input.edadMin;
            entity.tipoAsociado = input.tipoAsociado;
            entity.nroCupones = input.nroCupones;
            entity.marca = input.marca;
            entity.email = input.email;
            entity.tel = input.tel;
            entity.direccion = input.direccion;

            if(input.foto!=entity.foto) entity.foto = Helper.movePhoto(input.foto, "promos");

            UnitOfWork.AppPromocioneRepository.Update(entity);
            UnitOfWork.Save();


            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppPromocioneRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Seguro que deseas aliminar la promoción <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppPromocioneRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}