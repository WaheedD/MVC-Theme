using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appPromocioneInput
    {
        public int Id { get; set; }
        public string desc { get; set; }
        public string detalle { get; set; }
        public string foto { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public int sexo { get; set; }
        public int edadMax { get; set; }
        public int edadMin { get; set; }
        public int tipoAsociado { get; set; }
        public int nroCupones { get; set; }
        public string marca { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string direccion { get; set; }
    }
}