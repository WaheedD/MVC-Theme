using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class genTicketsVentaInput
    {
        public genTicketsVentaInput()
        {
            fecha = DateTime.Today;
        }
        [Required]
        public string idEntrada { get; set; }
        [Required]
        public int cantidad_real { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public double unitario { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public string concepto { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string tipoDoc { get; set; }
        [Required]
        public string numDoc { get; set; }
        [Required]
        public string tel { get; set; }
        [Required]
        public string email { get; set; }
        public string numeracion { get; set; }
        [Required]
        public bool confirmada { get; set; }
        [Required]
        public bool cargoCuenta { get; set; }
        [UIHint("Lookup")]
        [Lookup(Controller = "genTransaccionesVisanet")]
        public int? pagoOnline_Id { get; set; }
        [UIHint("Lookup")]
        [Lookup(Controller = "autUsuario")]
        public string socio_Id { get; set; }
        [UIHint("Lookup")]
        [Lookup(Controller = "genTicketsZona")]
        public string zona { get; set; }
        [Required]
        public int idPrecio { get; set; }
    }
}