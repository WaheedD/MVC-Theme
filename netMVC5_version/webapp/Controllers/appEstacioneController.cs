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
    public class appEstacioneController : AuthorizedBaseController
    {
        private static object MapToGridModel(appEstacione o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    o.descrip,
                    o.esSede,
                    o.lat,
                    o.lon,
                    foto = Helper.ImgHtml(o.foto),
                };

        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppEstacioneRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appEstacione>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppEstacioneRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView(new appEstacioneInput { });
        }

        [HttpPost]
        public ActionResult Create(appEstacioneInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new appEstacione
            {
                nombre = input.nombre,
                descrip = input.descrip,
                esSede = input.esSede,
                CreatedAt = DateTimeOffset.Now,
                Deleted = false,
                lat = input.lat,
                lon = input.lon,
                foto = input.foto,
            };

            UnitOfWork.AppEstacioneRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppEstacioneRepository.GetById(id);

            var input = new appEstacioneInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                descrip = entity.descrip,
                esSede = entity.esSede,
                lat = entity.lat,
                lon = entity.lon,
                foto = entity.foto,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appEstacioneInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppEstacioneRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.descrip = input.descrip;
            entity.esSede = input.esSede;
            entity.UpdatedAt = DateTimeOffset.Now;
            entity.lat = input.lat;
            entity.lon = input.lon;
            entity.foto = input.foto;

            UnitOfWork.AppEstacioneRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppEstacioneRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete estacione <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppEstacioneRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.AppEstacioneRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.nombre)));
        }
    }
    /*end*/
}