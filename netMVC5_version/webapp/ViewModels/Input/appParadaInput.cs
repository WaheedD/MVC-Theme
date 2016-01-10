using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appParadaInput
    {
        public int? Id { get; set; }
        public string nombre { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItemsHour", Controller = "appParada")]
        [Required]
        public int hora { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItemsMin", Controller = "appParada")]
        [Required]
        public int min { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "appEstacione")]
        public int? estacion_Id { get; set; }
        public int? ruta_Id { get; set; }
        [Required]
        public int duracion { get; set; }
        [Required]
        public bool esRetorno { get; set; }
    }
}