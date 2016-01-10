using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloReservaInput
    {
        public aloReservaInput()
        {
            this.fecha = DateTime.Today;
            this.fechaEntrada = DateTime.Today;
            this.fechaSalida = DateTime.Today;
        }
        public int? Id { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public DateTime fechaEntrada { get; set; }
        [Required]
        public DateTime fechaSalida { get; set; }
        public string codigoReserva { get; set; }
        public string comentarios { get; set; }
        public string DEUCON { get; set; }
        public string DEUPRV { get; set; }
        [Required]
        public double monto { get; set; }
        [Required]
        public double IGV { get; set; }
        [Required]
        public double total { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string alojamiento { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "autUsuario")]
        public string gestor { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "autUsuario")]
        public string socio { get; set; }
        [Required]
        public bool confirmada { get; set; }
        public string emision { get; set; }
    }
}