using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;

namespace SmartAdminMvc.ViewModels.Input
{
    public class appEstacioneInput
    {
        public int? Id { get; set; }
        public string nombre { get; set; }
        public string descrip { get; set; }
        [Required]
        public bool esSede { get; set; }
        [Required]
        public double lat { get; set; }
        [Required]
        public double lon { get; set; }
        [UIHint("FileUpload")]
        public string foto { get; set; }
    }
}