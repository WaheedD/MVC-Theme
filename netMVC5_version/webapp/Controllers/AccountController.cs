#region Using

using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

using SmartAdminMvc.Models;

#endregion

namespace SmartAdminMvc.Controllers
{
    [SmartAdminMvcAuthorize]
    public class AccountController : AuthorizedBaseController
    {
        // GET: /account/login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/admin")
        {
            if (IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View(GetRememberMeCookie(returnUrl));
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginModel viewModel, string returnUrl = "/admin", FormCollection f = null)
        {
            // fcamarena / 12345
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
            {
                var message = "";
                if (ModelState[""] == null)
                {
                    message = " An error occurred.";
                }
                else
                {
                    foreach (var error in ModelState[""].Errors)
                    {
                        message = error.ErrorMessage + "\r\n";
                    }
                }

                return Json(new
                {
                    Success = false,
                    Message = message,
                },
                JsonRequestBehavior.AllowGet);
            }

            try
            {
                var auth = AUTH(viewModel.jwt_token);
                if (auth != null)
                {
                    this.usuarioAPI = auth;
                    if (viewModel.RememberMe)
                    {
                        SetRememberMeCookie(viewModel.UserName, viewModel.Password);
                    }
                    else
                    {
                        RemoveRememberMeCookie();
                    }

                    SetTokenCookie(viewModel.jwt_token); // 15min
                    SetIsAuthenticated(true);
                    // If the user came from a specific page, redirect back to it
                    //return RedirectToLocal(returnUrl);

                    return Json(new
                    {
                        Success = true,
                        ReturnUrl = returnUrl,
                    },
                    JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.ToString(),
                },
                JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Success = false,
                Message = "An error occurred.",
            },
            JsonRequestBehavior.AllowGet);
        }

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (IsAuthenticated)
                Logout();
        }

        // POST: /account/Logout
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            SetIsAuthenticated(false);
            RemoveRememberMeCookie();
            RemoveTokenCookie();

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "/admin")
        {
            // If the return url starts with a slash "/" we assume it belongs to our site
            // so we will redirect to this "action"
            if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Redirect(HttpContext.Server.UrlDecode(returnUrl));

            // If we cannot verify if the url is local to our host we redirect to a default location
            return RedirectToAction("index", "home");
        }

        // GET: /account/lock
        [AllowAnonymous]
        public ActionResult Lock()
        {
            return View();
        }

        const string secretKey = "aj82kso94jnd6ue7039djs2husko261uq9esld92103sjalal2esfp";
        const string authDom = "http://api.crl.pe";

        public static usuarioAPI AUTH(string token = null)
        {
            //if (token == null) token = HttpContext.Current.Request.Headers["token"];//obtiene token del header

            if (token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricKey = System.Text.ASCIIEncoding.UTF8.GetBytes(secretKey);
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = authDom,
                ValidIssuer = authDom,
                IssuerSigningToken = new BinarySecretSecurityToken(symmetricKey)
            };
            SecurityToken securityToken;
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                //Obtener los CLAIMS de aqui(variable "principal")
                if (principal.HasClaim(o => o.Type == "ID"))
                {
                    return new usuarioAPI()
                    {
                        ID = principal.Claims.FirstOrDefault(o => o.Type == "ID").Value,
                        IdAsociado = principal.Claims.FirstOrDefault(o => o.Type == "IdAsociado").Value,
                        Consecutivo = short.Parse(principal.Claims.FirstOrDefault(o => o.Type == "Consecutivo").Value),
                        Nombre = principal.Claims.FirstOrDefault(o => o.Type == "Nombre").Value,
                        Apellidos = principal.Claims.FirstOrDefault(o => o.Type == "Apellido").Value,
                        personalCRL = principal.Claims.FirstOrDefault(o => o.Type == "personalCRL").Value == "1",
                        nivelAcceso = int.Parse(principal.Claims.FirstOrDefault(o => o.Type == "nivelAcceso").Value)
                    };
                }
                else return null;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }

    public class usuarioAPI
    {
        /// <summary>
        /// IdAsociado se usa para CARNET cuando se trata de asociado (ej: 12345) o para usuario cuando es personal del CRL (ej: 'fcamarena')
        /// </summary>
        public string IdAsociado { get; set; }
        public short Consecutivo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        /// <summary>
        /// Id en la table autUsuarios
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Si el usuario es administrador
        /// </summary>
        public bool personalCRL { get; set; }
        /// <summary>
        /// Nivel de acceso si es personalCRL. 5: Admin, 10: SuperAdmin
        /// </summary>
        public int nivelAcceso { get; set; }
    }
}