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
    public class appChofereController : AuthorizedBaseController
    {
        private static object MapToGridModel(appChoferes o)
        {
            return
                new
                {
                    o.Id,
                    o.nombre,
                    foto = Helper.ImgHtml(o.foto),
                    o.lic
                };

        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AppChofereRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.nombre.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<appChoferes>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.AppChofereRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
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
        public ActionResult Create(appChofereInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new appChoferes
            {
                nombre = input.nombre,
                foto = input.foto,
                lic = input.lic,
                usuario_Id=input.idUsuario
            };

            UnitOfWork.AppChofereRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AppChofereRepository.GetById(id);

            var input = new appChofereInput
            {
                Id = entity.Id,
                nombre = entity.nombre,
                foto = entity.foto,
                lic = entity.lic
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(appChofereInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AppChofereRepository.GetById(input.Id);

            entity.nombre = input.nombre;
            entity.foto = input.foto;
            entity.lic = input.lic;

            UnitOfWork.AppChofereRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AppChofereRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete chofere <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AppChofereRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
        public ActionResult GetItems()
        {
            return Json(UnitOfWork.AppChofereRepository.Get().ToList().Select(o => new KeyContent(o.Id, o.nombre)));
        }
    }
    /*end*/
}