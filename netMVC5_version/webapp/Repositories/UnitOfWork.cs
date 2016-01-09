using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SmartAdminMvc.Models;

namespace SmartAdminMvc.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private AdminContext context = new AdminContext();

        private GenericRepository<aloAlojamiento> alojamientoRepository;
        private GenericRepository<aloExcepcione> aloExcepcioneRepository;
        private GenericRepository<aloReserva> aloReservaRepository;
        private GenericRepository<aloSorteo> aloSorteoRepository;
        private GenericRepository<aloTipos> aloTipoRepository;
        private GenericRepository<aloZona> aloZonaRepository;
        private GenericRepository<appBus> appBusRepository;
        private GenericRepository<appChofere> appChofereRepository;
        private GenericRepository<appEstacione> appEstacioneRepository;
        private GenericRepository<appPromocione> appPromocioneRepository;
        private GenericRepository<appRuta> appRutaRepository;
        private GenericRepository<appParada> appParadaRepository;
        private GenericRepository<autUsuario> autUsuarioRepository;
        private GenericRepository<genActividade> genActividadeRepository;
        private GenericRepository<genTicketsVenta> genTicketsVentaRepository;
        private GenericRepository<genTicketsZona> genTicketsZonaRepository;
        private GenericRepository<genTransaccionesVisanet> genTransaccionesVisanetRepository;

        public GenericRepository<aloAlojamiento> AlojamientoRepository
        {
            get
            {
                if (alojamientoRepository == null)
                {
                    alojamientoRepository = new GenericRepository<aloAlojamiento>(context);
                }
                return alojamientoRepository;
            }

        }
        public GenericRepository<aloExcepcione> AloExcepcioneRepository
        {
            get
            {
                if (aloExcepcioneRepository == null)
                {
                    aloExcepcioneRepository = new GenericRepository<aloExcepcione>(context);
                }
                return aloExcepcioneRepository;
            }

        }
        public GenericRepository<aloReserva> AloReservaRepository
        {
            get
            {
                if (aloReservaRepository == null)
                {
                    aloReservaRepository = new GenericRepository<aloReserva>(context);
                }
                return aloReservaRepository;
            }

        }
        public GenericRepository<aloSorteo> AloSorteoRepository
        {
            get
            {
                if (aloSorteoRepository == null)
                {
                    aloSorteoRepository = new GenericRepository<aloSorteo>(context);
                }
                return aloSorteoRepository;
            }
        }
        public GenericRepository<aloTipos> AloTipoRepository
        {
            get
            {
                if (aloTipoRepository == null)
                {
                    aloTipoRepository = new GenericRepository<aloTipos>(context);
                }
                return aloTipoRepository;
            }
        }
        public GenericRepository<aloZona> AloZonaRepository
        {
            get
            {
                if (aloZonaRepository == null)
                {
                    aloZonaRepository = new GenericRepository<aloZona>(context);
                }
                return aloZonaRepository;
            }
        }
        public GenericRepository<autUsuario> AutUsuarioRepository
        {
            get
            {
                if (autUsuarioRepository == null)
                {
                    autUsuarioRepository = new GenericRepository<autUsuario>(context);
                }
                return autUsuarioRepository;
            }
        }

        public GenericRepository<appBus> AppBusRepository
        {
            get
            {
                if (appBusRepository == null)
                {
                    appBusRepository = new GenericRepository<appBus>(context);
                }
                return appBusRepository;
            }
        }

        public GenericRepository<appChofere> AppChofereRepository
        {
            get
            {
                if (appChofereRepository == null)
                {
                    appChofereRepository = new GenericRepository<appChofere>(context);
                }
                return appChofereRepository;
            }
        }

        public GenericRepository<appEstacione> AppEstacioneRepository
        {
            get
            {
                if (appEstacioneRepository == null)
                {
                    appEstacioneRepository = new GenericRepository<appEstacione>(context);
                }
                return appEstacioneRepository;
            }
        }
        
        public GenericRepository<appPromocione> AppPromocioneRepository
        {
            get
            {
                if (appPromocioneRepository == null)
                {
                    appPromocioneRepository = new GenericRepository<appPromocione>(context);
                }
                return appPromocioneRepository;
            }
        }
        
        public GenericRepository<appRuta> AppRutaRepository
        {
            get
            {
                if (appRutaRepository == null)
                {
                    appRutaRepository = new GenericRepository<appRuta>(context);
                }
                return appRutaRepository;
            }
        }
        
        public GenericRepository<appParada> AppParadaRepository
        {
            get
            {
                if (appParadaRepository == null)
                {
                    appParadaRepository = new GenericRepository<appParada>(context);
                }
                return appParadaRepository;
            }
        }
        
        public GenericRepository<genActividade> GenActividadeRepository
        {
            get
            {
                if (genActividadeRepository == null)
                {
                    genActividadeRepository = new GenericRepository<genActividade>(context);
                }
                return genActividadeRepository;
            }
        }

        public GenericRepository<genTicketsZona> GenTicketsZonaRepository
        {
            get
            {
                if (genTicketsZonaRepository == null)
                {
                    genTicketsZonaRepository = new GenericRepository<genTicketsZona>(context);
                }
                return genTicketsZonaRepository;
            }
        }

        public GenericRepository<genTransaccionesVisanet> GenTransaccionesVisanetRepository
        {
            get
            {
                if (genTransaccionesVisanetRepository == null)
                {
                    genTransaccionesVisanetRepository = new GenericRepository<genTransaccionesVisanet>(context);
                }
                return genTransaccionesVisanetRepository;
            }
        }

        public GenericRepository<genTicketsVenta> GenTicketsVentaRepository
        {
            get
            {
                if (genTicketsVentaRepository == null)
                {
                    genTicketsVentaRepository = new GenericRepository<genTicketsVenta>(context);
                }
                return genTicketsVentaRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string s = "";

                foreach (var eve in e.EntityValidationErrors)
                {
                    s += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}