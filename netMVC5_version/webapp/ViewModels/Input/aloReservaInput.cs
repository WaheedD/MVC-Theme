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
            this.confirmada = false;
        }
        public int? Id { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaEntrada { get; set; }
        public DateTime fechaSalida { get; set; }
        public string codigoReserva { get; set; }
        public string comentarios { get; set; }
        public string DEUCON { get; set; }
        public string DEUPRV { get; set; }
        public double monto { get; set; }
        public double IGV { get; set; }
        public double total { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string alojamiento { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string gestor { get; set; }
        [Required]
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public string socio { get; set; }
        public bool confirmada { get; set; }
        public string emision { get; set; }
    }
}