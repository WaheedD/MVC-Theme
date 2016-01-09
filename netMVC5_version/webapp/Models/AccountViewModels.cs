#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace SmartAdminMvc.Models
{
    public class AccountLoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string jwt_token { get; set; }
    }
}