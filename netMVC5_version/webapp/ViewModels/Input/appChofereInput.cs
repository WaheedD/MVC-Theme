using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appChofereInput
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string foto { get; set; }
        public string lic { get; set; }
    }
}