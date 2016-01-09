using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appBusInput
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descrip { get; set; }
        public string placa { get; set; }
        public double pasajeros { get; set; }
    }
}