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
            this.confirmada = false;
        }

        public string idEntrada { get; set; }
        public int cantidad_real { get; set; }
        public int cantidad { get; set; }
        public double unitario { get; set; }
        public DateTime fecha { get; set; }
        public string concepto { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoDoc { get; set; }
        public string numDoc { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string numeracion { get; set; }
        public bool confirmada { get; set; }
        public bool cargoCuenta { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "genTransaccionesVisanet")]
        public int? pagoOnline_Id { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "autUsuario")]
        public string socio_Id { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "genTicketsZona")]
        public string zona { get; set; }
        public int idPrecio { get; set; }
    }
}