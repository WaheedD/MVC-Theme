using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SmartAdminMvc.Models;
using SmartAdminMvc.Repositories;
using SmartAdminMvc.Helpers;

namespace SmartAdminMvc.Controllers
{
    [SmartAdminMvcAuthorize]
    public class AuthorizedBaseController :  BaseController
    {
        UnitOfWork unitOfWork = null;
        public UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork();
                }

                return unitOfWork;
            }
        }

        public usuarioAPI usuarioAPI
        {
            get
            {
                return (Session["usuarioAPI"] as usuarioAPI);
            }
            set
            {
                Session["usuarioAPI"] = value;
            }
        }

        public override bool IsAuthenticated
        {
            get
            {
                return (Session["IsAuthenticated"] as bool?) == true;
            }
        }

        public void SetIsAuthenticated(bool v)
        {
            Session.Add("IsAuthenticated", v);
        }
        public AccountLoginModel GetRememberMeCookie(string returnUrl)
        {
            if (Request.Cookies["authetication_username"] != null
                && !string.IsNullOrWhiteSpace(Request.Cookies["authetication_username"].Value)
                && Request.Cookies["authetication_password"] != null
                && !string.IsNullOrWhiteSpace(Request.Cookies["authetication_password"].Value)
                )
            {
                return new AccountLoginModel
                {
                    UserName = Encryption.Decrypt(Request.Cookies["authetication_username"].Value),
                    Password = Encryption.Decrypt(Request.Cookies["authetication_password"].Value),
                    RememberMe = true,
                    ReturnUrl = returnUrl,
                };
            }

            return new AccountLoginModel
            {
                RememberMe = true,
                ReturnUrl = returnUrl,
            };
        }

        public void SetRememberMeCookie(string username, string password)
        {
            Response.Cookies["authetication_username"].Value = Encryption.Encrypt(username);
            Response.Cookies["authetication_password"].Value = Encryption.Encrypt(password);

            Response.Cookies["authetication_username"].Expires = DateTime.Today.AddDays(15);
            Response.Cookies["authetication_password"].Expires = DateTime.Today.AddDays(15);
        }
        public void RemoveRememberMeCookie()
        {
            Response.Cookies["authetication_username"].Expires = DateTime.Today.AddDays(-1);
            Response.Cookies["authetication_password"].Expires = DateTime.Today.AddDays(-1);
        }
        public string GetTokenCookie()
        {
            if (Request.Cookies["authetication_token"] != null
                && !string.IsNullOrWhiteSpace(Request.Cookies["authetication_token"].Value))
            {
                return Encryption.Decrypt(Request.Cookies["authetication_token"].Value);
            }

            return null;
        }
        public void SetTokenCookie(string authetication_token)
        {
            Response.Cookies["authetication_token"].Value = Encryption.Encrypt(authetication_token);
            Response.Cookies["authetication_token"].Expires = DateTime.Today.AddMinutes(15);
        }
        public void RemoveTokenCookie()
        {
            Response.Cookies["authetication_token"].Expires = DateTime.Today.AddDays(-1);
        }
    }
}