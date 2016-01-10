using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appPromocioneInput
    {
        public appPromocioneInput()
        {
            desde = DateTime.Today;
            hasta = DateTime.Today;
        }
        public int? Id { get; set; }
        public string desc { get; set; }
        public string detalle { get; set; }
        public string foto { get; set; }
        [Required]
        public DateTime desde { get; set; }
        [Required]
        public DateTime hasta { get; set; }
        [Required]
        public int sexo { get; set; }
        [Required]
        public int edadMax { get; set; }
        [Required]
        public int edadMin { get; set; }
        [Required]
        public int tipoAsociado { get; set; }
        [Required]
        public int nroCupones { get; set; }
        public string marca { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string direccion { get; set; }
    }
}