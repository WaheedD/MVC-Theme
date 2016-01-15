using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloAlojamientoInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string nroAlojamiento { get; set; }
        [Required]
        public short personas { get; set; }
        [Required]
        public bool inhabilitado { get; set; }
        public string caracteristicas { get; set; }
        [Required]
        public double precioTempAlta { get; set; }
        [Required]
        public double precioTempBaja { get; set; }
        [Required]
        public double lat { get; set; }
        [Required]
        public double lon { get; set; }
        [UIHint("Lookup")]
        [Lookup( Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int tipo { get; set; }
        [UIHint("FileUpload")]
        public string foto { get; set; }
        public string agrup1 { get; set; }
        public string agrup2 { get; set; }
        //[UIHint("Lookup")]
        //[Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string zona { get; set; }
    }
}