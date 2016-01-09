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
    public class genActividadeController : AuthorizedBaseController
    {
        private static object MapToGridModel(genActividade o)
        {
            return
                new
                {
                    Id = o.id,
                    o.desc,
                    o.detalle,
                    o.idSede,
                    o.lugar,
                    o.fecha,
                    o.refCosto,
                    foto = Helper.ImgHtml(o.foto),
                    o.tel,
                    o.mail,
                    o.ventaDesde,
                    o.ventaHasta,
                    o.blogPost,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.GenActividadeRepository.Get(o => o.ventaTickets); // Only records where "ventaTickets"=1 

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.desc.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<genActividade>(items, g)
                {
                    Key = "id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.GenActividadeRepository.GetById(int.Parse(g.Key)), // called by the grid.api.update ( edit popupform success js func )
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
        public ActionResult Create(genActividadeInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new genActividade
            {
                desc = input.desc,
                detalle = input.detalle,
                idSede = input.idSede,
                lugar = input.lugar,
                fecha = input.fecha,
                refCosto = input.refCosto,
                foto = input.foto,
                tel = input.tel,
                mail = input.mail,
                ventaTickets = true, // Only records where "ventaTickets"=1 
                ventaDesde = input.ventaDesde,
                ventaHasta = input.ventaHasta,
                blogPost = input.blogPost,
            };

            UnitOfWork.GenActividadeRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.GenActividadeRepository.GetById(id);

            var input = new genActividadeInput
            {
                id = entity.id,
                desc = entity.desc,
                detalle = entity.detalle,
                idSede = entity.idSede,
                lugar = entity.lugar,
                fecha = entity.fecha,
                refCosto = entity.refCosto,
                foto = entity.foto,
                tel = entity.tel,
                mail = entity.mail,
                ventaDesde = entity.ventaDesde,
                ventaHasta = entity.ventaHasta,
                blogPost = entity.blogPost,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(genActividadeInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.GenActividadeRepository.GetById(input.id);

            entity.desc = input.desc;
            entity.detalle = input.detalle;
            entity.idSede = input.idSede;
            entity.lugar = input.lugar;
            entity.fecha = input.fecha;
            entity.refCosto = input.refCosto;
            entity.foto = input.foto;
            entity.tel = input.tel;
            entity.mail = input.mail;
            entity.ventaDesde = input.ventaDesde;
            entity.ventaHasta = input.ventaHasta;
            entity.blogPost = input.blogPost;

            UnitOfWork.GenActividadeRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.GenActividadeRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete actividade <b>{0}</b> ?", entity.id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.GenActividadeRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}