using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class genTicketsVentaController : AuthorizedBaseController
    {
        private static object MapToGridModel(genTicketsVentas o)
        {
            return
                new
                {
                   Id = o.idEntrada,
                    o.cantidad_real,
                    o.cantidad,
                    o.unitario,
                    o.fecha,
                    o.concepto,
                    o.nombre,
                    o.apellido,
                    o.tipoDoc,
                    o.numDoc,
                    o.tel,
                    o.email,
                    o.numeracion,
                    o.confirmada,
                    o.cargoCuenta,
                    o.pagoOnline_Id,
                    o.socio_Id,
                    o.zona_id,
                    o.idPrecio,
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.GenTicketsVentaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.concepto.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<genTicketsVentas>(items, g)
                {
                    Key = "idEntrada", // needed for api select, update, tree, nesting, EF
                    GetItem = () => UnitOfWork.GenTicketsVentaRepository.GetById(g.Key), // called by the grid.api.update ( edit popupform success js func )
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
        public ActionResult Create(genTicketsVentaInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new genTicketsVentas
            {
                idEntrada = input.idEntrada,
                cantidad_real = input.cantidad_real,
                cantidad = input.cantidad,
                unitario = input.unitario,
                fecha = input.fecha,
                concepto = input.concepto,
                nombre = input.nombre,
                apellido = input.apellido,
                tipoDoc = input.tipoDoc,
                numDoc = input.numDoc,
                tel = input.tel,
                email = input.email,
                numeracion = input.numeracion,
                confirmada = input.confirmada,
                cargoCuenta = input.cargoCuenta,
                pagoOnline_Id = input.pagoOnline_Id??0,
                socio_Id = input.socio_Id,
                zona_id = input.zona,
                idPrecio = input.idPrecio,
            };

            UnitOfWork.GenTicketsVentaRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(string id)
        {
            var entity = UnitOfWork.GenTicketsVentaRepository.GetById(id);

            var input = new genTicketsVentaInput
            {
                idEntrada = entity.idEntrada,
                cantidad_real = entity.cantidad_real,
                cantidad = entity.cantidad,
                unitario = entity.unitario,
                fecha = entity.fecha,
                concepto = entity.concepto,
                nombre = entity.nombre,
                apellido = entity.apellido,
                tipoDoc = entity.tipoDoc,
                numDoc = entity.numDoc,
                tel = entity.tel,
                email = entity.email,
                numeracion = entity.numeracion,
                confirmada = entity.confirmada,
                cargoCuenta = entity.cargoCuenta,
                pagoOnline_Id = entity.pagoOnline_Id,
                socio_Id = entity.socio_Id,
                zona = entity.zona_id??0,
                idPrecio = entity.idPrecio,
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(genTicketsVentaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.GenTicketsVentaRepository.GetById(input.idEntrada);

            //entity.idEntrada = input.idEntrada;
            entity.cantidad_real = input.cantidad_real;
            entity.cantidad = input.cantidad;
            entity.unitario = input.unitario;
            entity.fecha = input.fecha;
            entity.concepto = input.concepto;
            entity.nombre = input.nombre;
            entity.apellido = input.apellido;
            entity.tipoDoc = input.tipoDoc;
            entity.numDoc = input.numDoc;
            entity.tel = input.tel;
            entity.email = input.email;
            entity.numeracion = input.numeracion;
            entity.confirmada = input.confirmada;
            entity.cargoCuenta = input.cargoCuenta;
            entity.pagoOnline_Id = input.pagoOnline_Id??0;
            entity.socio_Id = input.socio_Id;
            entity.zona_id = input.zona;
            entity.idPrecio = input.idPrecio;

            UnitOfWork.GenTicketsVentaRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { input.idEntrada });
        }

        public ActionResult Delete(string id, string gridId)
        {
            var entity = UnitOfWork.GenTicketsVentaRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete tickets venta <b>{0}</b> ?", entity.idEntrada)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.GenTicketsVentaRepository.Delete(input.Id);
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}