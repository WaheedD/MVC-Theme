using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloAlojamientoInput
    {
        public aloAlojamientoInput()
        {
            this.inhabilitado = false;
        }

        public string Id { get; set; }
        public short personas { get; set; }
        public bool inhabilitado { get; set; }
        public string caracteristicas { get; set; }
        public double precioTempAlta { get; set; }
        public double precioTempBaja { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        [UIHint("Lookup")]
        [Lookup( Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int? tipo { get; set; }
        public string foto { get; set; }
        public string agrup1 { get; set; }
        public string agrup2 { get; set; }
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string zona { get; set; }
    }
}