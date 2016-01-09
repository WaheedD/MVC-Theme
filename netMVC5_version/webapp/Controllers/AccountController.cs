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
        public ActionResult Login(string returnUrl = "/")
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
        public ActionResult Login(AccountLoginModel viewModel, string returnUrl = "/")
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
            {
                var message = "";
                foreach (var error in ModelState[""].Errors)
                {
                    message = error.ErrorMessage + "\r\n";
                }

                return Json(new
                {
                    Success = false,
                    Message = message,
                },
                JsonRequestBehavior.AllowGet);
            }

            const string secretKey = "aj82kso94jnd6ue7039djs2husko261uq9esld92103sjalal2esfp";
            const string authDom = "http://api.crl.pe";
            var tokenHandler = new System.IdentityModel.Tokens.JwtSecurityTokenHandler();
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
                var principal = tokenHandler.ValidateToken(viewModel.jwt_token, validationParameters, out securityToken);
                if (principal.HasClaim(o => o.Type == "ID"))
                {
                    var v = principal.Claims.First().Value;
                }

                if (principal != null)
                {
                    if (viewModel.RememberMe)
                    {
                        SetRememberMeCookie(viewModel.UserName, viewModel.Password);
                    }

                    SetTokenCookie(securityToken.ToString()); // 15min
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

        // POST: /account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            SetIsAuthenticated(false);

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
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
    }
}