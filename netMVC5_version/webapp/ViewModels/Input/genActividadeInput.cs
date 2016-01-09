using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class genActividadeInput
    {
        public int id { get; set; }
        public string desc { get; set; }
        public string detalle { get; set; }
        public string idSede { get; set; }
        public string lugar { get; set; }
        public DateTime fecha { get; set; }
        public string refCosto { get; set; }
        public string foto { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public DateTime ventaDesde { get; set; }
        public DateTime ventaHasta { get; set; }
        public string blogPost { get; set; }
    }
}