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

        private GenericRepository<aloAlojamientos> alojamientoRepository;
        private GenericRepository<aloExcepciones> aloExcepcioneRepository;
        private GenericRepository<aloReservas> aloReservaRepository;
        private GenericRepository<aloSorteos> aloSorteoRepository;
        private GenericRepository<aloTipos> aloTipoRepository;
        private GenericRepository<aloZonas> aloZonaRepository;
        private GenericRepository<appBuses> appBusRepository;
        private GenericRepository<appChoferes> appChofereRepository;
        private GenericRepository<appEstaciones> appEstacioneRepository;
        private GenericRepository<appPromociones> appPromocioneRepository;
        private GenericRepository<appRutas> appRutaRepository;
        private GenericRepository<appParadas> appParadaRepository;
        private GenericRepository<autUsuarios> autUsuarioRepository;
        private GenericRepository<genActividades> genActividadeRepository;
        private GenericRepository<genTicketsVentas> genTicketsVentaRepository;
        private GenericRepository<genTicketsZonas> genTicketsZonaRepository;
        private GenericRepository<genTransaccionesVisanet> genTransaccionesVisanetRepository;

        public GenericRepository<aloAlojamientos> AlojamientoRepository
        {
            get
            {
                if (alojamientoRepository == null)
                {
                    alojamientoRepository = new GenericRepository<aloAlojamientos>(context);
                }
                return alojamientoRepository;
            }

        }
        public GenericRepository<aloExcepciones> AloExcepcioneRepository
        {
            get
            {
                if (aloExcepcioneRepository == null)
                {
                    aloExcepcioneRepository = new GenericRepository<aloExcepciones>(context);
                }
                return aloExcepcioneRepository;
            }

        }
        public GenericRepository<aloReservas> AloReservaRepository
        {
            get
            {
                if (aloReservaRepository == null)
                {
                    aloReservaRepository = new GenericRepository<aloReservas>(context);
                }
                return aloReservaRepository;
            }

        }
        public GenericRepository<aloSorteos> AloSorteoRepository
        {
            get
            {
                if (aloSorteoRepository == null)
                {
                    aloSorteoRepository = new GenericRepository<aloSorteos>(context);
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
        public GenericRepository<aloZonas> AloZonaRepository
        {
            get
            {
                if (aloZonaRepository == null)
                {
                    aloZonaRepository = new GenericRepository<aloZonas>(context);
                }
                return aloZonaRepository;
            }
        }
        public GenericRepository<autUsuarios> AutUsuarioRepository
        {
            get
            {
                if (autUsuarioRepository == null)
                {
                    autUsuarioRepository = new GenericRepository<autUsuarios>(context);
                }
                return autUsuarioRepository;
            }
        }

        public GenericRepository<appBuses> AppBusRepository
        {
            get
            {
                if (appBusRepository == null)
                {
                    appBusRepository = new GenericRepository<appBuses>(context);
                }
                return appBusRepository;
            }
        }

        public GenericRepository<appChoferes> AppChofereRepository
        {
            get
            {
                if (appChofereRepository == null)
                {
                    appChofereRepository = new GenericRepository<appChoferes>(context);
                }
                return appChofereRepository;
            }
        }

        public GenericRepository<appEstaciones> AppEstacioneRepository
        {
            get
            {
                if (appEstacioneRepository == null)
                {
                    appEstacioneRepository = new GenericRepository<appEstaciones>(context);
                }
                return appEstacioneRepository;
            }
        }
        
        public GenericRepository<appPromociones> AppPromocioneRepository
        {
            get
            {
                if (appPromocioneRepository == null)
                {
                    appPromocioneRepository = new GenericRepository<appPromociones>(context);
                }
                return appPromocioneRepository;
            }
        }
        
        public GenericRepository<appRutas> AppRutaRepository
        {
            get
            {
                if (appRutaRepository == null)
                {
                    appRutaRepository = new GenericRepository<appRutas>(context);
                }
                return appRutaRepository;
            }
        }
        
        public GenericRepository<appParadas> AppParadaRepository
        {
            get
            {
                if (appParadaRepository == null)
                {
                    appParadaRepository = new GenericRepository<appParadas>(context);
                }
                return appParadaRepository;
            }
        }
        
        public GenericRepository<genActividades> GenActividadeRepository
        {
            get
            {
                if (genActividadeRepository == null)
                {
                    genActividadeRepository = new GenericRepository<genActividades>(context);
                }
                return genActividadeRepository;
            }
        }

        public GenericRepository<genTicketsZonas> GenTicketsZonaRepository
        {
            get
            {
                if (genTicketsZonaRepository == null)
                {
                    genTicketsZonaRepository = new GenericRepository<genTicketsZonas>(context);
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

        public GenericRepository<genTicketsVentas> GenTicketsVentaRepository
        {
            get
            {
                if (genTicketsVentaRepository == null)
                {
                    genTicketsVentaRepository = new GenericRepository<genTicketsVentas>(context);
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