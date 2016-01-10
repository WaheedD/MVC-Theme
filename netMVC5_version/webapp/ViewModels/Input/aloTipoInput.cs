using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloTipoInput
    {
        public aloTipoInput()
        {
        }
        public int? Id { get; set; }
        public string nombre { get; set; }
        public string clase { get; set; }
        public string croquis { get; set; }
        [Required]
        public double precioTempAlta { get; set; }
        [Required]
        public double precioTempBaja { get; set; }
        public string idSede { get; set; }
        [Required]
        public bool restricFDStempAlta { get; set; }
    }
}