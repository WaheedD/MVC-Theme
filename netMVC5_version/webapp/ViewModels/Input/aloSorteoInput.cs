using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloSorteoInput
    {
        public aloSorteoInput()
        {
            this.fecha = DateTime.Today;
            this.fechaPago = DateTime.Today;
            this.estadia_desde = DateTime.Today;
            this.estadia_hasta = DateTime.Today;
            this.inscripcion_desde = DateTime.Today;
            this.inscripcion_hasta = DateTime.Today;
            this.hide_tipo = false;
        }

        public int? Id { get; set; }
        public string detalle { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public DateTime fechaPago { get; set; }
        [Required]
        public DateTime estadia_desde { get; set; }
        [Required]
        public DateTime estadia_hasta { get; set; }
        [Required]
        public DateTime inscripcion_desde { get; set; }
        [Required]
        public DateTime inscripcion_hasta { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int? tipo { get; set; }
        public bool hide_tipo { get; set; }
    }
}