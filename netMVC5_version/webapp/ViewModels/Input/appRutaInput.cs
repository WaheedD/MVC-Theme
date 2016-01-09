using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appRutaInput
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descrip { get; set; }
        [UIHint("AjaxCheckboxList")]
        [AweUrl(Action = "GetItemsDayName", Controller = "appRuta")]
        public IEnumerable<int> dias { get; set; }
        public bool activa { get; set; }
        public double currLat { get; set; }
        public double currLon { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "appBus")]
        public int? currBus_Id { get; set; }
        [UIHint("AjaxDropdown")]
        [AweUrl(Action = "GetItems", Controller = "appChofere")]
        public int? currChofer_Id { get; set; }
        public DateTime lastBoard { get; set; }
    }
}