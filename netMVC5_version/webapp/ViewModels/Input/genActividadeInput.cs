using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class genActividadeInput
    {
        public genActividadeInput()
        {
            fecha = DateTime.Today;
            ventaDesde = DateTime.Today;
            ventaHasta = DateTime.Today;
        }
        public int? id { get; set; } 
        [Required]
        public string desc { get; set; }
        public string detalle { get; set; }
        public string idSede { get; set; }
        public string lugar { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        public string refCosto { get; set; }
        [UIHint("FileUpload")]
        public string foto { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        [Required]
        public DateTime ventaDesde { get; set; }
        [Required]
        public DateTime ventaHasta { get; set; }
        public string blogPost { get; set; }
    }
}