using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class aloZonaInput
    {
        [Required]
        public string Id { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        [Required]
        public int personas { get; set; }
        public string croquis { get; set; }
        [Required]
        public double precio { get; set; }
        [UIHint("Lookup")]
        [Lookup(Fullscreen = true, CustomSearch = true, TableLayout = true)]
        public int? tipo { get; set; }
    
    }
}