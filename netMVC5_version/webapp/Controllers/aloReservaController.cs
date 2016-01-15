using System;
using System.Linq;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.ViewModels.Input;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.Controllers
{
    /*begin*/
    public class aloReservaController : AuthorizedBaseController
    {
        private static object MapToGridModel(aloReservas o)
        {
            return
                new
                {
                    o.Id,
                    o.codigoReserva,
                    o.fecha,
                    o.fechaEntrada,
                    o.fechaSalida,
                    o.comentarios,
                   total="S/. "+ o.total,
                    confirmada = o.confirmada ? "Sí" : "",
                    o.emision,
                    aloj = o.aloAlojamientos.nroAlojamiento,
                    tipo = o.aloAlojamientos.aloTipos.nombre
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = UnitOfWork.AloReservaRepository.Get();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.codigoReserva.ToLower().Contains(search));
            }

            return Json(new GridModelBuilder<aloReservas>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => UnitOfWork.AloReservaRepository.GetById(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                Map = MapToGridModel
            }.Build());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView(new aloReservaInput { });
        }

        [HttpPost]
        public ActionResult Create(aloReservaInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var entity = new aloReservas
                {
                    fecha = input.fecha,
                    fechaEntrada = input.fechaEntrada,
                    fechaSalida = input.fechaSalida,
                    codigoReserva = input.codigoReserva,
                    comentarios = input.comentarios,
                    DEUCON = input.DEUCON,
                    DEUPRV = input.DEUPRV,
                    monto = input.monto,
                    IGV = input.IGV,
                    total = input.total,
                    confirmada = input.confirmada,
                    emision = input.emision,
                    alojamiento_Id = input.alojamiento,
                    gestor_Id = input.gestor,
                    socio_Id = input.socio,
                };

            UnitOfWork.AloReservaRepository.Insert(entity);
            UnitOfWork.Save();

            return Json(MapToGridModel(entity)); // returning grid model, used in grid.api.renderRow
        }

        public ActionResult Edit(int id)
        {
            var entity = UnitOfWork.AloReservaRepository.GetById(id);

            var input = new aloReservaInput
            {
                Id = entity.Id,
                fecha = entity.fecha,
                fechaEntrada = entity.fechaEntrada,
                fechaSalida = entity.fechaSalida,
                codigoReserva = entity.codigoReserva,
                comentarios = entity.comentarios,
                DEUCON = entity.DEUCON,
                DEUPRV = entity.DEUPRV,
                monto = entity.monto,
                IGV = entity.IGV,
                total = entity.total,
                confirmada = entity.confirmada,
                emision = entity.emision,
                alojamiento = entity.alojamiento_Id,
                gestor = entity.gestor_Id,
                socio = entity.socio_Id,

                //Chef = Db.Get<Chef>(entity.Chef),
                //Meals = Db.Meals.Where(o => entity.Meals.Contains(o.Id)),
                //BonusMeal = Db.Get<Meal>(entity.BonusMealId)
            };

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(aloReservaInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var entity = UnitOfWork.AloReservaRepository.GetById(input.Id);

            entity.fecha = input.fecha;
            entity.fechaEntrada = input.fechaEntrada;
            entity.fechaSalida = input.fechaSalida;
            entity.codigoReserva = input.codigoReserva;
            entity.comentarios = input.comentarios;
            entity.DEUCON = input.DEUCON;
            entity.DEUPRV = input.DEUPRV;
            entity.monto = input.monto;
            entity.IGV = input.IGV;
            entity.total = input.total;
            entity.confirmada = input.confirmada;
            entity.emision = input.emision;
            entity.alojamiento_Id = input.alojamiento;
            entity.gestor_Id = input.gestor;
            entity.socio_Id = input.socio;

            UnitOfWork.AloReservaRepository.Update(entity);
            UnitOfWork.Save();

            // returning the key to call grid.api.update
            return Json(new { entity.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var entity = UnitOfWork.AloReservaRepository.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id.ToString(),
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete reserva <b>{0}</b> ?", entity.Id)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            UnitOfWork.AloReservaRepository.Delete(int.Parse(input.Id));
            UnitOfWork.Save();

            return Json(new { input.Id });
        }
    }
    /*end*/
}