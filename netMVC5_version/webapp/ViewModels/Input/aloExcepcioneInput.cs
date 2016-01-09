using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloExcepcioneInput
    {
        public aloExcepcioneInput()
        {
            this.desde = DateTime.Today;
            this.hasta = DateTime.Today;
            this.hide_tipo = false;
        }

        public int? Id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public short porcentaje { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup( Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int? tipo { get; set; }
        public bool hide_tipo { get; set; }
        public string tipoExcep { get; set; }
    }
}